﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Business.Abstract;
using EfLearning.Core.Classrooms;
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
    public class MaterialAnswerController : ControllerBase
    {
        private readonly IMaterialAnswerService _materialAnswerService;
        private readonly IMapper _mapper;
        public MaterialAnswerController(IMaterialAnswerService materialAnswerService, IMapper mapper)
        {
            _materialAnswerService = materialAnswerService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MaterialAnswerResource model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            MaterialAnswer materialAnswer = _mapper.Map<MaterialAnswerResource, MaterialAnswer>(model);
            materialAnswer.UserId= userId;
            var materialAnswerResponse = await _materialAnswerService.CreateAsync(materialAnswer);

            if (!materialAnswerResponse.Success)
                return BadRequest(materialAnswerResponse.Message);

            return Ok(materialAnswerResponse.Extra);
        }
        /// <summary>
        /// Because one student can answer one time... UserId and materialId is composite keys of
        /// MaterialAnswerTable
        /// </summary>
        /// <param name="materialId">Student's answer of material</param>
        /// <returns></returns>
        [HttpDelete("{materialId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int materialId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var materialAnswerResponse = await _materialAnswerService.DeleteByIdAsync(userId,materialId);

            if (!materialAnswerResponse.Success)
                return BadRequest(materialAnswerResponse.Message);

            return Ok(materialAnswerResponse.Extra);
        }
        [HttpGet("{materialId:int}")]
        public async Task<IActionResult> GetMaterialAnswers([FromRoute]int materialId)
        {
            var materialAnswersResponse = await _materialAnswerService.GetByMaterialId(materialId);
            if (!materialAnswersResponse.Success)
            {
                return BadRequest(materialAnswersResponse.Message);
            }
            var simpleMaterialAnswer = materialAnswersResponse.Extra.Select(m => new
            {
                m.MaterialId,
                m.Answer,
                m.CreationTime,
                m.UserId,
                m.User.Name,
                m.User.Surname,
                m.User.UserName,
                m.Score
            });

            return Ok(simpleMaterialAnswer);
        }
        [HttpPut]
        public async Task<IActionResult> GivePoint([FromBody]MaterialAnswerUpdateResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            MaterialAnswer materialAnswer = _mapper.Map<MaterialAnswerUpdateResource, MaterialAnswer>(model);
            var materialAnswerResponse = await _materialAnswerService.UpdateAsync(materialAnswer);

            if (!materialAnswerResponse.Success)
                return BadRequest(materialAnswerResponse.Message);

            return Ok(materialAnswerResponse.Extra);
        }
        [HttpGet("{givenClassroomId:int}")]
        public async Task<IActionResult> GetScoreList([FromRoute]int givenClassroomId)
        {
            var materialAnswersResponse = await _materialAnswerService.GetSumOfPointsByGivenClassroomId(givenClassroomId,1);
            if (!materialAnswersResponse.Success)
            {
                return BadRequest(materialAnswersResponse.Message);
            }
            return Ok(materialAnswersResponse.Extra);
        }
        [HttpGet]
        private async Task<IActionResult> GetUserSuccess()
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var materialAnswerResponse = await _materialAnswerService.GetTotalScore(userId);
            if (!materialAnswerResponse.Success)
            {
                return BadRequest(materialAnswerResponse.Message);
            }
           

            return Ok(materialAnswerResponse.Extra);
        }
        [HttpGet]
        public async Task<IActionResult> GetDoneMaterialCount()
        {
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));
            var takenClassroomListResponse = await _materialAnswerService.GetMaterialCountAsync(userId);
            if (!takenClassroomListResponse.Success)
            {
                return BadRequest(takenClassroomListResponse.Message);
            }
            return Ok(takenClassroomListResponse.Extra.Count);
        }
    }
}