using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Business.Abstract;
using EfLearning.Core.Classrooms;
using EfLearning.Core.EntitiesHelper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
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
        /// <param name="userId">Student's id</param>
        /// <returns></returns>
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetClassrooms([FromRoute]int userId)
        {
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TakenClassroom takenClassroom = _mapper.Map<TakenClassroomResource, TakenClassroom>(model);
            var takenClassroomResponse = await _takenClassroomService.CreateAsync(takenClassroom);

            if (!takenClassroomResponse.Success)
                return BadRequest(takenClassroomResponse.Message);

            return Ok(takenClassroomResponse.Extra);
        }
        /// <summary>
        /// Leave Classroom UserId and givenClassroomId is composite keys of TakenClassroom Table...
        /// One student joins a classroom one time
        /// </summary>
        /// <param name="userId">Student's id</param>
        /// <param name="givenClassroomId">student whose id, student who take this classroom </param>
        /// <returns></returns>
        [HttpDelete("{userId:int}/{givenClassroomId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute]int givenClassroomId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var takenClassroomResponse = await _takenClassroomService.DeleteAsync(userId, givenClassroomId);

            if (!takenClassroomResponse.Success)
                return BadRequest(takenClassroomResponse.Message);

            return Ok(takenClassroomResponse.Extra);
        }
    }
}