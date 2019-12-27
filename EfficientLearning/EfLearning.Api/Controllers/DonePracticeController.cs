using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.Practice;
using EfLearning.Business.Abstract;
using EfLearning.Core.Practices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DonePracticeController : ControllerBase
    {
        private readonly IDonePracticeService _donePracticeService;
        private readonly IMapper _mapper;
        public DonePracticeController(IDonePracticeService donePracticeService, IMapper mapper)
        {
            _donePracticeService = donePracticeService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]DonePracticeResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            DonePractice donePractice = _mapper.Map<DonePracticeResource, DonePractice>(model);
            donePractice.UserId =userId;
            var courseResponse = await _donePracticeService.CreateAsync(donePractice);

            if (!courseResponse.Success)
                return BadRequest(courseResponse.Message);
            return Ok(courseResponse.Extra);
        }
        /// <summary>
        /// Reminder for practice
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var notificationResponse = await _donePracticeService.GetByDateAsync(userId);

            if (!notificationResponse.Success)
                return BadRequest(notificationResponse.Message);
            return Ok(notificationResponse.Extra);
        }
        /// <summary>
        /// Bring last 10 done practices which user did before
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLastPractices()
        {

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var response = await _donePracticeService.GetLastPracticesAsync(userId, 10);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Extra);
        }
    }
}