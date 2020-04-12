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
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        ISpreadItRepository _repository;
        PostFactory _postFactory = new PostFactory();
        PostImagesFactory _imageFactory = new PostImagesFactory();
        readonly LinkGenerator _linkGenerator;

        public PostsController(LinkGenerator linkGenerator)
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult Get(int? id, string sort = "CreatedDate", 
            string fields = null,
            int page = 1, int pageSize = 5)
        {
            try
            {
                if (id.HasValue)
                { 
                    var post = _postFactory.CreatePost(
                        _repository.GetPost(id.Value));
                    return Ok(post);
                }
                else
                {

                    List<string> lstOfFields = new List<string>();
                    bool includeImages = false;
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                        includeImages = lstOfFields.Any(f => f.Contains("images"));
                    }

                    var posts = _repository.GetPosts();

                    var factorizedPosts = posts.ApplySort(sort)
                                    .Select(a => _postFactory.CreateDataShapedObject(a, lstOfFields));

                    var totalCount = factorizedPosts.Count();
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


                    return Ok(factorizedPosts
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
                    Method = "GetPosts"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post([FromForm] DTO.Post post)
        {
            try
            {          
                if (post != null && !post.CategoryId.Equals(0))
                {
                    var pos = _postFactory.CreatePost(post);
                    var result = _repository.InsertPost(pos);

                    if (result.Status == RepositoryActionStatus.Created && !pos.Id.Equals(0))
                    {
                        var files = Request.Form.Files;
                        var folderName = Path.Combine("Resources", "Images");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        if (files.Any(f => f.Length == 0))
                        {
                            return BadRequest();
                        }

                        foreach (var file in files)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require

                            DTO.PostImage PostImage = new DTO.PostImage()
                            {
                                Path = dbPath,
                                PostId = pos.Id
                            };
                        
                            var image = _imageFactory.CreatePostImage(PostImage);
                            var ImageResult = _repository.InsertImage(image);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                        }

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
                    Method = "PostPost"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut]
        public IActionResult PutPost([FromBody] DTO.Post post)
        {
            try
            {
                if (post != null && !post.Id.Equals(0) && !post.CategoryId.Equals(0))
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