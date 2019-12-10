using AutoMapper;
using EfLearning.Api.Resources.Account;
using EfLearning.Api.Resources.User;
using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly ICustomIdentityManager _userManager;
        private readonly IMapper _mapper;
        public AccountController(ICustomIdentityManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
                return BadRequest(ModelState);

            AppUser user = _mapper.Map<UserResource, AppUser>(model);
            UserResponse userResponse = await _userManager.RegisterAsync(user,model.Password);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.User);
            
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
        private async Task<ActionResult> ConfirmEmail([FromQuery]int userId, [FromQuery]string token)
        {
            if (userId == 0 || token == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ConfirmUserAsync(userId,token);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
            
        }
       
      
    }
}
