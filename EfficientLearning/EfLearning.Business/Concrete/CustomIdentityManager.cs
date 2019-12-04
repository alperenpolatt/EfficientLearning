using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using EfLearning.Data;
using EfLearning.Data.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class CustomIdentityManager : ICustomIdentityManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private  IRefreshTokenDal _refreshTokenDal;
        private UserManager<AppUser> _aspUserManager;
        private RoleManager<AppRole> _aspRoleManager;
        public readonly TokenValidationParameters _tokenValidationParameters;
        public CustomIdentityManager( IUnitOfWork unitOfWork, UserManager<AppUser> aspUserManager, RoleManager<AppRole> aspRoleManager,  IRefreshTokenDal refreshTokenDal, TokenValidationParameters tokenValidationParameters)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenDal = refreshTokenDal;
            _aspUserManager = aspUserManager;
            _aspRoleManager = aspRoleManager;
            _tokenValidationParameters = tokenValidationParameters;

        }

        public async Task<BaseResponse> ConfirmUserAsync(int userId, string token)
        {
            var result = await _aspUserManager.ConfirmEmailAsync(await _aspUserManager.FindByIdAsync(userId.ToString()), token);
            if (!result.Succeeded)
            {
                return new BaseResponse(false, String.Empty);
            }
            return new BaseResponse(true, String.Empty);
        }

        public async Task<UserResponse> CreateStudentAsync(AppUser user, string password)
        {
                user.CreationTime = DateTime.UtcNow;
                var resultCreate=await _aspUserManager.CreateAsync(user, password);
                var resultRole = await _aspUserManager.AddToRoleAsync(user,CustomRoles.Student);
                if (resultCreate.Succeeded && resultRole.Succeeded)
                {
                    _unitOfWork.Complete();
                    return new UserResponse(user);
                }
            return new UserResponse(
                resultCreate.Errors.FirstOrDefault().Description
                );
        }

        public async Task<AuthenticationResponse> LoginAsync(string email, string password)
        {
            var user = await _aspUserManager.FindByEmailAsync(email);

            if (user == null)
                return new AuthenticationResponse("User does not exist");
            var userHasValidPassword = await _aspUserManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
                return new AuthenticationResponse("User/password combination is wrong");

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResponse("Invalid Token");
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTime > DateTime.UtcNow)
            {
                return new AuthenticationResponse("This token hasn't expired yet");
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;


            var storedRefreshToken = await _refreshTokenDal.SingleAsync(refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResponse("This refresh token does not exist");
            }
            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResponse( "This refresh token has expired" );
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResponse("This refresh token has been invalidated");
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResponse( "This refresh token has been used" );
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResponse( "This refresh token does not match this JWT" ) ;
            }

            storedRefreshToken.Used = true;
           await _refreshTokenDal.UpdateAsync(storedRefreshToken,storedRefreshToken.Token);
            await _unitOfWork.CompleteAsync();

            var user = await _aspUserManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        private async Task<AuthenticationResponse> GenerateAuthenticationResultForUserAsync(AppUser user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecurityKey));

                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString())
            };

                var userClaims = await _aspUserManager.GetClaimsAsync(user);
                claims.AddRange(userClaims);

                var userRoles = await _aspUserManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _aspRoleManager.FindByNameAsync(userRole);
                    if (role == null) continue;
                    var roleClaims = await _aspRoleManager.GetClaimsAsync(role);

                    foreach (var roleClaim in roleClaims)
                    {
                        if (claims.Contains(roleClaim))
                            continue;

                        claims.Add(roleClaim);
                    }
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(JwtSettings.AccessTokenExpiration)),
                    SigningCredentials =
                        new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                
                var refreshToken = new RefreshToken
                {
                    JwtId = token.Id,
                    UserId = user.Id,
                    CreationTime = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(JwtSettings.RefreshTokenExpiration)
                };

                await _refreshTokenDal.AddAsync(refreshToken);
                await _unitOfWork.CompleteAsync();

                return new AuthenticationResponse
                {
                    Success = true,
                    Token = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken.Token
                };
            }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch(Exception ex)
            {
                var a = ex;
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
