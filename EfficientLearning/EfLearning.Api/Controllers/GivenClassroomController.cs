using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Business.Abstract;
using EfLearning.Core.Classrooms;
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
    public class GivenClassroomController : ControllerBase
    {
        private readonly IGivenClassroomService _givenClassroomService;
        private readonly IMapper _mapper;
        public GivenClassroomController(IGivenClassroomService givenClassroomService, IMapper mapper)
        {
            _givenClassroomService = givenClassroomService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var givenClassroomList = await _givenClassroomService.GetAllAsync();
            if (!givenClassroomList.Success)
                return BadRequest(givenClassroomList.Message);
            return Ok(givenClassroomList.Extra);
        }
       
        /// <summary>
        /// For teacher bring classrooms which teachers have
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClassrooms()
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var givenClassroomListResponse = await _givenClassroomService.GetByUserIdAsync(userId);
            if (!givenClassroomListResponse.Success)
            {
                return BadRequest(givenClassroomListResponse.Message);
            }
            return Ok(givenClassroomListResponse.Extra);
        }
        /// <summary>
        /// bring classrooms with students who take this classroom
        /// </summary>
        /// <param name="givenClassroomId">GivenClassroomId</param>
        /// <returns></returns>
        [HttpGet("{givenClassroomId:int}")]
        public async Task<IActionResult> GetStudents([FromRoute]int givenClassroomId)
        {
            var givenClassroomResponse = await _givenClassroomService.GetByIdAsync(givenClassroomId);
            if (!givenClassroomResponse.Success)
            {
                return BadRequest(givenClassroomResponse.Message);
            }
            var students = givenClassroomResponse.Extra.TakenClassrooms.Select(u=>new  { 
                Id=u.User.Id,
                Name=u.User.Name,
                Surname = u.User.Surname,
                UserName = u.User.UserName
            });

            return Ok(students);
        }
        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> FindAClassroom([FromRoute]string searchTerm)
        {
            var givenClassroomResponse = await _givenClassroomService.GetBySearchTermAsync(searchTerm);
            if (!givenClassroomResponse.Success)
            {
                return BadRequest(givenClassroomResponse.Message);
            }
            return Ok(givenClassroomResponse.Extra);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]GivenClassroomResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            GivenClassroom givenClassroom = _mapper.Map<GivenClassroomResource, GivenClassroom>(model);
            givenClassroom.UserId= userId;
            var givenClassroomResponse = await _givenClassroomService.CreateAsync(givenClassroom);
            if (!givenClassroomResponse.Success)
                return BadRequest(givenClassroomResponse.Message);

            return Ok(givenClassroomResponse.Extra);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]GivenClassroomUpdateResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            GivenClassroom givenClassroom = _mapper.Map<GivenClassroomUpdateResource, GivenClassroom>(model);
            var givenClassroomResponse = await _givenClassroomService.UpdateAsync(givenClassroom);

            if (!givenClassroomResponse.Success)
                return BadRequest(givenClassroomResponse.Message);

            return Ok(givenClassroomResponse.Extra);
        }
        [HttpDelete("{givenClassroomId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int givenClassroomId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var givenClassroomResponse = await _givenClassroomService.DeleteByIdAsync(givenClassroomId);

            if (!givenClassroomResponse.Success)
                return BadRequest(givenClassroomResponse.Message);

            return Ok(givenClassroomResponse.Extra);
        }
        
    }
}