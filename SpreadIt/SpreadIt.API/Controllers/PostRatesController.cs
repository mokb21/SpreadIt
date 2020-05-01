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
    public class PostRatesController : Controller
    {
        ISpreadItRepository _repository;
        PostRateFactory _postRateFactory;

        public PostRatesController()
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());

            _postRateFactory = new PostRateFactory();
        }


        [HttpPost]
        public IActionResult Post([FromBody] DTO.PostRate postRate)
        {
            try
            {
                if (postRate != null && Enum.IsDefined(typeof(RatingStatus), postRate.Status))
                {
                    var result = _repository.InsertPostRate(_postRateFactory.CreatePostRate(postRate));

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        return Created("", _postRateFactory.CreatePostRate(result.Entity));
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
                    Method = "PostPostRate"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}