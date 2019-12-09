using EfLearning.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorizeTestController : ControllerBase
    {
        [HttpGet("OnlyStudentCanSee")]
        [Authorize(Policy = CustomRoles.Student)]
        public ActionResult OnlyStudent()
        {
            return Ok("Success");
        }
        [HttpGet("OnlyAdminCanSee")]
        [Authorize(Policy  = CustomRoles.Admin)]
        public ActionResult OnlyAdmin()
        {
            return Ok("Success");
        }
    }
}