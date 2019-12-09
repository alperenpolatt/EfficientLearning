using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources;
using EfLearning.Business.Abstract;
using EfLearning.Core.Users;
using EfLearning.Data;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        public async Task<IActionResult> GetAllStudents()
        {
            var userListResponse = await _userService.GetUsersByRoleAsync(CustomRoles.Student);
            if (!userListResponse.Success)
            {
                return BadRequest(userListResponse.Message);
            }
            return Ok(userListResponse.Users);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var userListResponse = await _userService.GetUsersByRoleAsync(CustomRoles.Teacher);
            if (!userListResponse.Success)
            {
                return BadRequest(userListResponse.Message);
            }
            return Ok(userListResponse.Users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody]UserResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AppUser user = _mapper.Map<UserResource, AppUser>(model);
            var userResponse = await _userService.CreateStudentAsync(user, model.Password);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.User);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody]UserResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AppUser user = _mapper.Map<UserResource, AppUser>(model);
            var userResponse = await _userService.CreateTeacherAsync(user, model.Password);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.User);
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
            var userResponse = await _userService.UpdateUserAsync(user);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.User);
        }
        /// <summary>
        /// This deletes teacher and student
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserDeleteResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userResponse = await _userService.DeleteUserByIdAsync(model.Id);

            if (!userResponse.Success)
                return BadRequest(userResponse.Message);

            return Ok(userResponse.User);
        }
    }
}