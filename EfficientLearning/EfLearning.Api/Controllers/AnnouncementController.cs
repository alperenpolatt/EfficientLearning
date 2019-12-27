using System.Threading.Tasks;
using EfLearning.Business.Abstract;
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
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }
        [HttpGet("{givenClassroomId:int}")]
        public async Task<IActionResult> GetAll([FromRoute]int givenClassroomId)
        {
            var announcementResponse = await _announcementService.GetAllAsync(givenClassroomId);
            if (!announcementResponse.Success)
            {
                return BadRequest(announcementResponse.Message);
            }

            return Ok(announcementResponse.Extra);
        }
    }
}