using NutritecComments.Models;
using NutritecComments.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NutritecComments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // Get all comments
        [HttpGet("getcomments")]
        public ActionResult<List<Comment>> GetAll() =>
            _commentService.GetAll();

        // Get filtered comments
        [HttpGet("getcomments/{patientEmail}/{day}/{meal}")]
        public ActionResult<List<Comment>> GetFiltered(string patientEmail, string day, string meal) =>
            _commentService.GetFilteredComments(patientEmail, day, meal);

        // Post a comment
       [HttpPost("postcomment")]
       public ActionResult<Comment> Create(Comment comment)
        {
            _commentService.Create(comment);

            return comment;
        }

    }
}
