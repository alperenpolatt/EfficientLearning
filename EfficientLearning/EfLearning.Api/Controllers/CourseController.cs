using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.Course;
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
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courseList =await _courseService.GetAllAsync();
            if (!courseList.Success)
                return BadRequest(courseList.Message);
            return Ok(courseList.Extra);

        }

        [HttpGet]
        public  IActionResult GetAllProgrammingTypes()
        {
            return Ok(TypeExtension.ToList<ProgrammingType>());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CourseResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Course course = _mapper.Map<CourseResource, Course>(model);
            var courseResponse = await _courseService.CreateAsync(course);

            if (!courseResponse.Success)
                return BadRequest(courseResponse.Message);

            return Ok(courseResponse.Extra);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CourseUpdateResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Course course = _mapper.Map<CourseUpdateResource, Course>(model);
            var courseResponse = await _courseService.UpdateAsync(course);
            
            if (!courseResponse.Success)
                return BadRequest(courseResponse.Message);

            return Ok(courseResponse.Extra);
        }
        [HttpDelete("{courseId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseResponse = await _courseService.DeleteByIdAsync(courseId);

            if (!courseResponse.Success)
                return BadRequest(courseResponse.Message);

            return Ok(courseResponse.Extra);
        }
        [HttpGet]
        public async Task<IActionResult> GetPopularityofProgrammingLanguages()
        {
            var courseList = await _courseService.GetPopularityofProgrammingTypesAsync();
            if (!courseList.Success)
                return BadRequest(courseList.Message);
            return Ok(courseList.Extra.Select(c => new {
                ProgrammingLanguage= c.ProgrammingType.ToString(),
                Count = c.Count
            }));
        }
    }
}