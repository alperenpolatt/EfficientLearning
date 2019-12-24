using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.User;
using EfLearning.Business.Abstract;
using EfLearning.Core.Users;
using EfLearning.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Policy = CustomRoles.Admin)]
        public async Task<IActionResult> GetAllStudents()
        {
            var userListResponse = await _userService.GetUsersByRoleAsync(CustomRoles.Student);
            if (!userListResponse.Success)
            {
                return BadRequest(userListResponse.Message);
            }
            return Ok(userListResponse.Extra);
        }
        [HttpGet]
        [Authorize(Policy = CustomRoles.Admin)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var userListResponse = await _userService.GetUsersByRoleAsync(CustomRoles.Teacher);
            if (!userListResponse.Success)
            {
                return BadRequest(userListResponse.Message);
            }
            return Ok(userListResponse.Extra);
        }
        [HttpPost]
        [Authorize(Policy = CustomRoles.Admin)]
        public async Task<IActionResult> CreateStudent([FromBody]UserResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AppUser user = _mapper.Map<UserResource, AppUser>(model);
            var userResponse = await _userService.CreateStudentAsync(user, model.Password);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.Extra);
        }
        [HttpPost]
        [Authorize(Policy = CustomRoles.Admin)]
        public async Task<IActionResult> CreateTeacher([FromBody]UserResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AppUser user = _mapper.Map<UserResource, AppUser>(model);
            var userResponse = await _userService.CreateTeacherAsync(user, model.Password);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.Extra);
        }
        /// <summary>
        /// This deletes teacher and student
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UserUpdateResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AppUser user = _mapper.Map<UserUpdateResource, AppUser>(model);
            user.Id= Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var userResponse = await _userService.UpdateUserAsync(user);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.Extra);
        }
        /// <summary>
        /// This deletes teacher and student
        /// </summary>
        [HttpDelete("{userId:int}")]
        [Authorize(Policy = CustomRoles.Admin)]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userResponse = await _userService.DeleteUserByIdAsync(userId);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.Extra);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserWithRole()
        {
            var email= HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var userResponse = await _userService.GetUserByEmailWithRoleAsync(email);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.Extra);
        }

        /// <summary>
        /// Bring students who registered the system in terms of last month which is parameter
        /// </summary>
        [HttpGet("{month:int}")]
        [Authorize(Policy = CustomRoles.Admin)]
        public async Task<IActionResult> GetRegisteredStudentsByMonth([FromRoute] int month)
        {
            var userListResponse = await _userService.GetRegisteredUsersByMonthAsync(month,CustomRoles.Student);
            if (!userListResponse.Success)
                return BadRequest(userListResponse.Message);

            return Ok(userListResponse.Extra);
        }


    }
}