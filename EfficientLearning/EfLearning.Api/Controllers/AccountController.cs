using AutoMapper;
using EfLearning.Api.EmailServices;
using EfLearning.Api.Extensions;
using EfLearning.Api.Resources;
using EfLearning.Business;
using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using EfLearning.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICustomIdentityManager _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private IEmailSender _emailSender;
        private readonly ILogger _logger;
        public AccountController(ICustomIdentityManager userManager, SignInManager<AppUser> signInManager, IMapper mapper, IEmailSender emailSender, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "Under  ", "Development", "...." };
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser user = _mapper.Map<UserResource, AppUser>(model);
            UserResponse userResponse = await _userManager.CreateStudentAsync(user,model.Password);
            if (userResponse.Success)
            {
                return Ok(userResponse.User);
            }
            else
            {
                return BadRequest(userResponse.Errors.ToList());
            }
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var result = await _signInManager.PasswordSignInAsync(model.Email,
                                  model.Password, true, lockoutOnFailure: true);
            if (!result.Succeeded)
                return BadRequest( new AuthenticationResponse(ErrorMessages.UnknownError));
            return Ok(new AuthenticationResponse());
            
        }
        [HttpPost("LoginToken")]
        public async Task<IActionResult> LoginToken([FromBody] LoginResource model)
        {
            var authResponse = await _userManager.LoginAsync(model.Email, model.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenResource model)
        {
            var authResponse = await _userManager.RefreshTokenAsync(model.Token, model.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }
        [HttpGet]
        [Route("register-email/{userId}/{token}", Name = "ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromRoute]int userId, [FromRoute]string token)
        {
            if (userId == 0 || token == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ConfirmUserAsync(userId,token);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                return Ok(new BaseResponse(true,String.Empty));
            }
            return BadRequest(new BaseResponse(false, String.Empty));
        }
        [HttpGet("CurrentUser")]
        public ActionResult GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(User.Identity.Name);
            }
            return Unauthorized();
        }
      
    }
}
