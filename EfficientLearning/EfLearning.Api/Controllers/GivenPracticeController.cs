using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfLearning.Api.Resources.Practice;
using EfLearning.Business.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    public class GivenPracticeController : ControllerBase
    {
        private readonly IGivenPracticeService _givenPracticeService;
        private readonly IDonePracticeService _donePracticeService;
        public GivenPracticeController(IGivenPracticeService givenPracticeService, IDonePracticeService donePracticeService)
        {
            _givenPracticeService = givenPracticeService;
            _donePracticeService = donePracticeService;
        }

        [HttpGet("{programmingType:int}")]
        public async Task<IActionResult> GetLevels([FromRoute]int programmingType)
        {
            var givenPracticeResponse = await _givenPracticeService.GetLevelsAsync(programmingType);
            if (!givenPracticeResponse.Success)
            {
                return BadRequest(givenPracticeResponse.Message);
            }

            return Ok(givenPracticeResponse.Extra);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLevelDetail([FromRoute]int id)
        {
            var givenPracticeResponse = await _givenPracticeService.GetLevelDetailAsync(id);
            if (!givenPracticeResponse.Success)
            {
                return BadRequest(givenPracticeResponse.Message);
            }

            return Ok(givenPracticeResponse.Extra);
        }
        [HttpPost]
        public async Task<IActionResult> CheckAnswer([FromBody]GivenPracticeAnswerResource model)
        {
            var givenPracticeResponse = await _givenPracticeService.CheckAnswerAsync(model.Id,model.Answer.ToString());
            if (!givenPracticeResponse.Success)
            {
                return BadRequest(givenPracticeResponse.Message);
            }

            return Ok(givenPracticeResponse.Extra);
        }
        [HttpGet("{givenPracticeId:int}")]
        public async Task<IActionResult> GetAHint(int givenPracticeId)
        {
            var givenPracticeResponse = await _givenPracticeService.GetAHintAsync(givenPracticeId);
            if (!givenPracticeResponse.Success)
            {
                return BadRequest(givenPracticeResponse.Message);
            }
            return Ok(givenPracticeResponse.Extra);
        }


    }
}