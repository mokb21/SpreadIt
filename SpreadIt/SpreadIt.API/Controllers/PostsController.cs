using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;
using SpreadIt.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace SpreadIt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        ISpreadItRepository _repository;
        PostFactory _postFactory = new PostFactory();
        readonly LinkGenerator _linkGenerator;
        //const int maxPageSize = 10;

        public PostsController(LinkGenerator linkGenerator)
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult Get(int? id, string sort = "CreatedDate", string fields = null,
            int page = 1, int pageSize = 5)
        {
            try
            {
                if (id.HasValue)
                    return Ok(_postFactory.CreatePost(
                        _repository.GetPost(id.Value)));
                else
                {

                    List<string> lstOfFields = new List<string>();
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var posts = _repository.GetPosts()
                                    .ApplySort(sort)
                                    .Select(a => _postFactory.CreateDataShapedObject(a, lstOfFields));

                    var totalCount = posts.Count();
                    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);


                    string prevLink = page > 1 ? _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "Posts",
                        values: new
                        {
                            page = page - 1,
                            pageSize = pageSize,
                            sort = sort,
                            fields = fields
                        }) : "";

                    var nextLink = page < totalPages ? _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "Posts",
                        values: new
                        {
                            page = page + 1,
                            pageSize = pageSize,
                            sort = sort,
                            fields = fields
                        }) : "";

                    var paginationHeader = new
                    {
                        currentPage = page,
                        pageSize = pageSize,
                        totalCount = totalCount,
                        totalPages = totalPages,
                        prevLink = prevLink,
                        nextLink = nextLink
                    };

                    Response.Headers.Add("X-Pagination",
                        Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


                    return Ok(posts
                            .Skip(pageSize * (page - 1))
                            .Take(pageSize));
                }
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
                    //if (post.LocationId.HasValue)
                    //{
                    //    var location = _repository.GetLocation(post.LocationId.Value);
                    //    if (location != null)
                    //    {
                    //        post.Longitude = location.Longitude;
                    //        post.Latitude = location.Latitude;
                    //    }
                    //}
                    var pos = _postFactory.CreatePost(post);
                    var result = _repository.InsertPost(pos);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newPost = _postFactory.CreatePost(result.Entity);
                        var newPostLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Post",
                        controller: "Posts",
                        values: new
                        {
                            id = newPost.Id
                        });

                        return Created(newPostLink, newPost);
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

        [HttpPut]
        public IActionResult PutComment([FromBody] DTO.Post post)
        {
            try
            {
                if (post != null && !post.Id.Equals(0))
                {
                    var pos = _postFactory.CreatePost(post);
                    var result = _repository.UpdatePost(pos);

                    if (result.Status == RepositoryActionStatus.Updated)
                    {
                        var newPost = _postFactory.CreatePost(result.Entity);
                        var newPostLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "PutComment",
                        controller: "Posts",
                        values: new
                        {
                            id = newPost.Id
                        });

                        return Created(newPostLink, newPost);
                    }
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError);
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
                    Method = "Put"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}