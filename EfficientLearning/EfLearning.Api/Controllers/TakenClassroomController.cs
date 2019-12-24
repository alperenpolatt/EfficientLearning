using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Business.Abstract;
using EfLearning.Core.Classrooms;
using EfLearning.Core.EntitiesHelper;
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
    public class TakenClassroomController : ControllerBase
    {
        private readonly ITakenClassroomService _takenClassroomService;
        private readonly IMapper _mapper;
        public TakenClassroomController(ITakenClassroomService takenClassroomService, IMapper mapper)
        {
            _takenClassroomService = takenClassroomService;
            _mapper = mapper;
        }


        /// <summary>
        /// bring classrooms which students have
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClassrooms()
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var takenClassroomListResponse = await _takenClassroomService.GetByUserIdAsync(userId);
            if (!takenClassroomListResponse.Success)
            {
                return BadRequest(takenClassroomListResponse.Message);
            }
            var takenClassrooms = takenClassroomListResponse.Extra.Select(u => new {
                TakenClassroomUserId=u.UserId,
                u.GivenClassroomId,
                GivenClassroomDescription=u.GivenClassroom.Description,
                CourseName=u.GivenClassroom.Course.Name,
                CourseProgrammingLanguage=u.GivenClassroom.Course.ProgrammingType.ToString()
            });
            return Ok(takenClassrooms);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TakenClassroomResource model)
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TakenClassroom takenClassroom = _mapper.Map<TakenClassroomResource, TakenClassroom>(model);
            takenClassroom.UserId = userId;
            var takenClassroomResponse = await _takenClassroomService.CreateAsync(takenClassroom);

            if (!takenClassroomResponse.Success)
                return BadRequest(takenClassroomResponse.Message);

            return Ok(takenClassroomResponse.Extra);
        }
        /// <summary>
        /// One student joins a classroom one time
        /// </summary>
        /// <param name="givenClassroomId">student whose id, student who take this classroom </param>
        /// <returns></returns>
        [HttpDelete("{givenClassroomId:int}")]
        public async Task<IActionResult> Delete([FromRoute]int givenClassroomId)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var takenClassroomResponse = await _takenClassroomService.DeleteAsync(userId, givenClassroomId);

            if (!takenClassroomResponse.Success)
                return BadRequest(takenClassroomResponse.Message);

            return Ok(takenClassroomResponse.Extra);
        }
    }
}