using AutoMapper;
using EfLearning.Api.Resources.Announcement;
using EfLearning.Business.Abstract;
using EfLearning.Core.Announcements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EfLearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [HttpGet("{announcementId:int}")]
        public async Task<IActionResult> GetAll([FromRoute]int announcementId)
        {
            var commentResponse = await _commentService.GetAllAsync(announcementId);
            if (!commentResponse.Success)
            {
                return BadRequest(commentResponse.Message);
            }

            return Ok(commentResponse.Extra);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CommentResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = Int32.Parse((HttpContext.User.FindFirst("id").Value));

            Comment comment = _mapper.Map<CommentResource, Comment>(model);
            comment.UserId= userId;
            
            var commentResponse = await _commentService.CreateAsync(comment);

            if (!commentResponse.Success)
                return BadRequest(commentResponse.Message);

            return Ok(commentResponse.Extra);
        }

     
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentResponse = await _commentService.DeleteByIdAsync(commentId);
            if (!commentResponse.Success)
                return BadRequest(commentResponse.Message);

            return Ok(commentResponse.Extra);
        }




    }
}