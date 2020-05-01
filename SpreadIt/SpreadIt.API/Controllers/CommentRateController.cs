using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentRateController : ControllerBase
    {
        ISpreadItRepository _repository;
        CommentRateFactory _commentRateFactory;

        public CommentRateController()
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());

            _commentRateFactory = new CommentRateFactory();
        }


        [HttpPost]
        public IActionResult Post([FromBody] DTO.CommentRate commentRate)
        {
            try
            {
                if (commentRate != null && Enum.IsDefined(typeof(RatingStatus), commentRate.Status))
                {
                    var result = _repository.InsertCommentRate(_commentRateFactory.CreateCommentRate(commentRate));

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        return Created("", _commentRateFactory.CreateCommentRate(result.Entity));
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "PostCommentRate"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}