using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpreadIt.API.Helpers;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : Controller
    {

        ISpreadItRepository _repository;
        CategoryFactory _categoryFactory = new CategoryFactory();
        readonly LinkGenerator _linkGenerator;

        public CategoriesController(LinkGenerator linkGenerator)
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult Get(int? id, string sort = "Name", string fields = null,
            int page = 1, int pageSize = 5)
        {
            try
            {
                List<string> lstOfFields = new List<string>();
                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                var categories = _repository.GetCategories()
                                .ApplySort(sort)
                                .Select(a => _categoryFactory.CreateDataShapedObject(a, lstOfFields));

                var totalCount = categories.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);


                string prevLink = page > 1 ? _linkGenerator.GetPathByAction(
                    HttpContext,
                    action: "Get",
                    controller: "Categories",
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
                    controller: "Categories",
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


                return Ok(categories
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize));
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "GetCategories"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]DTO.Category category)
        {
            try
            {
                if (category != null)
                {
                    var _category = _categoryFactory.CreateCategory(category);
                    var result = _repository.InsertCategory(_category);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newCategory = _categoryFactory.CreateCategory(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Post",
                        controller: "Categories",
                        values: new
                        {
                            id = newCategory.Id
                        });

                        return Created(newCommentLink, newCategory);
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
                    Method = "PostCategories"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]DTO.Category category)
        {
            try
            {
                if (category != null && !category.Id.Equals(0))
                {
                    var _category = _categoryFactory.CreateCategory(category);
                    var result = _repository.UpdateCategory(_category);

                    if (result.Status == RepositoryActionStatus.Updated)
                    {
                        var newCategory = _categoryFactory.CreateCategory(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "Categories",
                        values: new
                        {
                            id = newCategory.Id
                        });

                        return Ok(newCategory);
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
                    Method = "PutCategories"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteCategory(id);
                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    return NoContent();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "DeleteCategories"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
