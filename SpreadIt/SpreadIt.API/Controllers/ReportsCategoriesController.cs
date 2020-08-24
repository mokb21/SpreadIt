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

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsCategoriesController : Controller
    {

        ISpreadItRepository _repository;
        ReportCategoryFactory _reportsCategoriesFactory = new ReportCategoryFactory();
        readonly LinkGenerator _linkGenerator;

        public ReportsCategoriesController(LinkGenerator linkGenerator)
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult Get(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var category = _repository.GetReportCategory(id.Value);
                    if (category == null)
                        return NotFound();
                    else
                        return Ok(_reportsCategoriesFactory.CreateReportCategory(category));
                }
                else
                    return Ok(_repository.GetReportCategories()
                        .Select(element => _reportsCategoriesFactory.CreateReportCategory(element)));
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "GetReportsCategories"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]DTO.ReportCategory category)
        {
            try
            {
                if (category != null)
                {
                    var _category = _reportsCategoriesFactory.CreateReportCategory(category);
                    var result = _repository.InsertReportCategory(_category);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newCategory = _reportsCategoriesFactory.CreateReportCategory(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Post",
                        controller: "ReportsCategories",
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
                    Method = "PortReportsCategories"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
