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
using Microsoft.EntityFrameworkCore;
using SpreadIt.Repository.Models;
using System.Drawing;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult Get(int? id, string sort = "-CreatedDate",
            string fields = null,
            int page = 1, int pageSize = 5, string userId = "")
        {
            try
            {
                if (id.HasValue)
                {
                    var post = _repository.GetPost(id.Value);
                    if (post == null)
                        return NotFound();
                    else
                        return Ok(_postFactory.CreatePost(post, userId));
                }
                else
                {

                    List<string> lstOfFields = new List<string>();
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var posts = _repository.GetPosts(userId);

                    posts = posts.ApplySort(sort);

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
                            .Take(pageSize).ToList()
                            .Select(a => _postFactory.CreateDataShapedObject(a, userId, lstOfFields)));
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

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var files = Request.Form.Files;

                        if (files.Any(f => f.Length == 0))
                        {
                            return BadRequest();
                        }

                        foreach (var file in files)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var fullPath = Path.Combine(SpreadItConstants.ImagesFolderPath, fileName);
                            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{Constants.SpreadItConstants.PostImagesFolderGet}//";

                            PostImage image = new PostImage()
                            {
                                Path = baseUrl + fileName,
                                PostId = pos.Id
                            };

                            var ImageResult = _repository.InsertImage(image);


                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            Image resizedImage;

                            using (var img = Image.FromFile(fullPath))
                            {
                                resizedImage = (Image)ImageResize.ResizeImage(img, img.Height / 3, img.Width / 3);
                            }

                            System.IO.File.Delete(fullPath);
                            resizedImage.Save(fullPath);
                        }

                        var newPost = _postFactory.CreatePost(result.Entity, null);
                        var newPostLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
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
        public IActionResult Put([FromBody] DTO.Post post)
        {
            try
            {
                if (post != null && !post.Id.Equals(0))
                {
                    post.LastUpdatedDate = DateTime.Now;

                    var postDb = _postFactory.CreatePost(post);
                    var result = _repository.UpdatePost(postDb);

                    if (result.Status == RepositoryActionStatus.Updated)
                    {
                        var newPost = _postFactory.CreatePost(result.Entity, null);
                        var newPostLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
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
                    Method = "PutPost"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeletePost(id);

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
                    Method = "DeletePost"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}