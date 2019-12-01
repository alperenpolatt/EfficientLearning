using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EfLearning.Core.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EfLearning.Api.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;
        public TokenHandler(IOptions<TokenOptions> tokenOptions) //Dependecy Inj  için interface olmalı
        {
            _tokenOptions = tokenOptions.Value;
        }
        public AccessToken CreateAccessToken(AppUser user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SignHandler.GetSecurityKey(_tokenOptions.SecurityKey);
            var encodedSecurityKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,//Token oluşturuldaktan hemen sonra kullanılabilsin
                claims: GetClaims(user),//Token'ın payload'ında data olarak taşınacak bilgiler
                signingCredentials:encodedSecurityKey
                );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            AccessToken accessToken = new AccessToken() { 
                Token=token,
                Expiration=accessTokenExpiration,
                RefreshToken=CreateRefreshToken()
            };

            return accessToken;
        }

        public void RevokeRefreshToken(AppUser user)
        {
            //user.RefreshToken = null;
        }

        private IEnumerable<Claim> GetClaims(AppUser user)
        {
            //Token'ın payload'ında data olarak taşınacak bilgiler
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new  Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            return claims;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(numberByte);

                return Convert.ToBase64String(numberByte);
            }
        }
    }
}
