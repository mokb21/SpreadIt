using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CommentsController : Controller
    {

        ISpreadItRepository _repository;
        CommentFactory _commentFactory = new CommentFactory();
        readonly LinkGenerator _linkGenerator;

        public CommentsController(LinkGenerator linkGenerator)
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
            _linkGenerator = linkGenerator;
        }


        [HttpGet("{PostId}")]
        public IActionResult GetCommentByPost(int PostId)
        {
            try
            {
                if (!PostId.Equals(null))
                    return Ok(
                        _repository.GetCommentByPost(PostId).Select(a => _commentFactory.CreateComment(a)));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "Get"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult PostComment([FromBody]DTO.Comment comment)
        {
            try
            {
                if (comment != null && !comment.PostId.Equals(0))
                {
                    var commen = _commentFactory.CreateComment(comment);
                    var result = _repository.InsertComment(commen);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newComment = _commentFactory.CreateComment(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Post",
                        controller: "Comments",
                        values: new
                        {
                            id = newComment.Id
                        });

                        //return Created(newCommentLink, newComment);
                        return Created("", newComment);
                    }
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "Post"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
      
        [HttpPut]
        public IActionResult PutComment([FromBody]DTO.Comment comment)
        {
            try
            {
                if (comment != null && !comment.PostId.Equals(0) && !comment.Id.Equals(0))
                {
                    var commen = _commentFactory.CreateComment(comment);
                    var result = _repository.UpdateComment(commen);

                    if (result.Status == RepositoryActionStatus.Updated)
                    {
                        var newComment = _commentFactory.CreateComment(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Put",
                        controller: "Comments",
                        values: new
                        {
                            id = newComment.Id
                        });

                        return Ok(newComment);
                    }
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "Post"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                if (!id.Equals(0))
                {
                    var result = _repository.DeleteComment(id);
                    if (result.Status == RepositoryActionStatus.Deleted)
                    {
                        return Ok("Comment Has Been Deleted");
                    }
                    else
                        return NotFound();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "Delete"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
