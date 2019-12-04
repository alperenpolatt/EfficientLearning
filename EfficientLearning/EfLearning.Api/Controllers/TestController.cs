using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfLearning.Business;
using EfLearning.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        [HttpGet("OnlyStudent")]
        [Authorize(Policy = CustomRoles.Student)]
        public ActionResult OnlyStudent()
        {
            return Ok("Success");
        }
        [HttpGet("OnlyAdmin")]
        [Authorize(Policy  = CustomRoles.Admin)]
        public ActionResult OnlyAdmin()
        {
            return Ok("Success");
        }
    }
}