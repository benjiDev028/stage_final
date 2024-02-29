﻿using AutoMapper;
using CommentApi.Requests;
using CommentBusinessLogic.DTO;
using CommentBusinessLogic.Services.Interfaces;
using CommentDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentaireController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentaireController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [HttpPost("post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostComment(commentPostRequest commentPost)
        {
            var commentMap = _mapper.Map<CommentDto>(commentPost);

            var comment = await _commentService.CreateCommentAsync(commentMap);
            return Ok(comment); 

        }
        [HttpGet("selectComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getComment (Guid id)
        {
            var commentSel = await _commentService.GetCommentAsync(id);
            return Ok(commentSel);
        }

        [HttpGet("allComments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getAllComment()
        {
            var comments = await _commentService.GetAllCommentsAsync();

            return Ok(comments);
        }

        [HttpDelete("Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> commentDelete([FromBody] Guid id)
        {
            var commentDeleted = await _commentService.DeleteCommentAsync(id);
            return Ok(commentDeleted);
        }


        [HttpPut("upComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateComment(Commentaire comm)
        {
            var upCommentMap = _mapper.Map<CommentDto>(comm);
            return  Ok(await _commentService.UpdateCommentAsync(upCommentMap));

        }
    }
}