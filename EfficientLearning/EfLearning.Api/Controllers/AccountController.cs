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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [EnableCors]
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
                return Ok(userResponse);
            }
            else
            {
                return BadRequest(userResponse);
            }
            
        }
        [HttpPost("AccessToken")]
        public async Task<IActionResult> AccessToken([FromBody] LoginResource model)
        {
            var authResponse = await _userManager.LoginAsync(model.Email, model.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.Message);
            }

            return Ok(authResponse);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenResource model)
        {
            var authResponse = await _userManager.RefreshTokenAsync(model.Token, model.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.Message);
            }

            return Ok(authResponse);
        }
        [HttpGet]
        [Route("register-email/{userId}/{token}", Name = "ConfirmEmail")]
        private async Task<ActionResult> ConfirmEmail([FromRoute]int userId, [FromRoute]string token)
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
