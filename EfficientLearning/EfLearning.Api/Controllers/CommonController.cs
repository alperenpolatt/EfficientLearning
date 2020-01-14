using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EfLearning.Business.Abstract;
using EfLearning.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly IGivenClassroomService _givenClassroomService;
        public CommonController(ICommonService commonService, IGivenClassroomService givenClassroomService)
        {
            _commonService = commonService;
            _givenClassroomService = givenClassroomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPopularClasses()
        {
            var commentResponse = await _givenClassroomService.GetByMostStudentsAsync();
            if (!commentResponse.Success)
            {
                return BadRequest(commentResponse.Message);
            }

            return Ok(commentResponse.Extra);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSuccess()
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            var response = await _commonService.GetTotalScore(userId, role);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Extra);
        }
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var response = await _commonService.GetNotifications(userId);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Extra);
        }


    }
}