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
        public IActionResult Get(int PostId)
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
                    Method = "GetComment"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]DTO.Comment comment)
        {
            try
            {
                if (comment != null)
                {
                    var commen = _commentFactory.CreateComment(comment);
                    var result = _repository.InsertComment(commen);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newComment = _commentFactory.CreateComment(result.Entity);

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
                    Method = "PostComment"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]DTO.Comment comment)
        {
            try
            {
                if (comment != null && !comment.PostId.Equals(0) && !comment.Id.Equals(0))
                {
                    var commentDb = _commentFactory.CreateComment(comment);
                    var result = _repository.UpdateComment(commentDb);

                    if (result.Status == RepositoryActionStatus.Updated)
                    {
                        var newComment = _commentFactory.CreateComment(result.Entity);

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
                    Method = "PutComment"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteComment(id);

                if (result.Status == RepositoryActionStatus.Deleted)
                    return NoContent();
                else if (result.Status == RepositoryActionStatus.NotFound)
                    return NotFound();

                return BadRequest();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "DeleteComment"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
