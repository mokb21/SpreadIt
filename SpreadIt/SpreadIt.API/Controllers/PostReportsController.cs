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

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReportsController : Controller
    {

        ISpreadItRepository _repository;
        PostReportFactory _postReportFactory = new PostReportFactory();
        readonly LinkGenerator _linkGenerator;

        public PostReportsController(LinkGenerator linkGenerator)
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
                {
                    var postReports = _repository.GetPostReport(id.Value);
                    if (postReports == null)
                        return NotFound();
                    else
                        return Ok(_postReportFactory.CreatePostReport(postReports));
                }
                else
                {
                    List<string> lstOfFields = new List<string>();
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var postReports = _repository.GetPostReports()
                                    .ApplySort(sort);

                    var totalCount = postReports.Count();
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
                        controller: "PostReports",
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


                    return Ok(postReports
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize).ToList()
                        .Select(a => _postReportFactory.CreateDataShapedObject(a, lstOfFields)));
                }
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "GetPostReports"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]DTO.PostReport postReport)
        {
            try
            {
                if (postReport != null && !postReport.PostId.Equals(0) && !postReport.ReportCategoryId.Equals(0))
                {
                    var _postReport = _postReportFactory.CreatePostReport(postReport);
                    var result = _repository.InsertPostReport(_postReport);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newPostReport = _postReportFactory.CreatePostReport(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "PostReports",
                        values: new
                        {
                            id = newPostReport.Id
                        });

                        return Created(newCommentLink, newPostReport);
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
                    Method = "PostPostReports"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            try
            {

                var result = _repository.ChangePostReportStatus(id);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    var UpdatedPostReport = _postReportFactory.CreatePostReport(result.Entity);
                    var newCommentLink = _linkGenerator.GetPathByAction(
                    HttpContext,
                    action: "Get",
                    controller: "PostReports",
                    values: new
                    {
                        id = UpdatedPostReport.Id
                    });

                    return Created(newCommentLink, UpdatedPostReport);
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "PutPostReports"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}