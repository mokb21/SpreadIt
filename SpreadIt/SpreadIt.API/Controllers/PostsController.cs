using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;
using SpreadIt.API.Helpers;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        ISpreadItRepository _repository;
        PostFactory _postFactory = new PostFactory();

        public PostsController()
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
        }

        [HttpGet]
        public IActionResult Get(int? id, string sort = "CreatedDate")
        {
            try
            {
                if (id.HasValue)
                    return Ok(_postFactory.CreatePost(
                        _repository.GetPost(id.Value)));
                else
                    return Ok(_repository.GetPosts()
                        .ApplySort(sort)
                        .Select(a => _postFactory.CreatePost(a)));
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
        public IActionResult Post([FromBody] DTO.Post post)
        {
            try
            {
                if (post != null)
                {
                    var pos = _postFactory.CreatePost(post);
                    var result = _repository.InsertPost(pos);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newPost = _postFactory.CreatePost(result.Entity);
                        return Created("api/Post?id=" + newPost.Id.ToString(),
                            newPost);
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
                    Method = "Post"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}