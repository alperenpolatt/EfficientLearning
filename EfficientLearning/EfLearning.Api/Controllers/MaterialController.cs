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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;
        public MaterialController(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MaterialResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Material material = _mapper.Map<MaterialResource, Material>(model);
            var materialResponse = await _materialService.CreateAsync(material,model.Description);

            if (!materialResponse.Success)
                return BadRequest(materialResponse.Message);

            var simplyResponse = new
            {
                materialResponse.Extra.Id,
                materialResponse.Extra.AnnouncementId,
                materialResponse.Extra.Announcement.Description,
                materialResponse.Extra.GivenClassroomId,
                materialResponse.Extra.Hint,
                materialResponse.Extra.MaterialScale,
               materialType= materialResponse.Extra.MaterialType.Description(),
                materialResponse.Extra.Question
            };

            return Ok(simplyResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]MaterialUpdateResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Material material = _mapper.Map<MaterialUpdateResource, Material>(model);
            var materialResponse = await _materialService.UpdateAsync(material,model.Description);

            if (!materialResponse.Success)
                return BadRequest(materialResponse.Message);

            return Ok(materialResponse.Extra);
        }
        [HttpDelete("{materialId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int materialId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var materialResponse = await _materialService.DeleteByIdAsync(materialId);

            if (!materialResponse.Success)
                return BadRequest(materialResponse.Message);

            return Ok(materialResponse.Extra);
        }
        /// <summary>
        /// bring materials which classroom has
        /// </summary>
        /// <param name="givenClassroomId">givenClassroom id</param>
        /// <returns></returns>
        [HttpGet("{givenClassroomId:int}")]
        public async Task<IActionResult> GetMaterials([FromRoute]int givenClassroomId)
        {
            var materialResponse = await _materialService.GetMaterialsByGivenClassroomId(givenClassroomId);
            if (!materialResponse.Success)
            {
                return BadRequest(materialResponse.Message);
            }
           
            return Ok(materialResponse.Extra);
        }
        [HttpGet]
        public IActionResult GetAllMaterialTypes()
        {
            return Ok(TypeExtension.ToList<MaterialType>());
        }
    }
}