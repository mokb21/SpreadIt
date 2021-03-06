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
    public class CommentReportsController : ControllerBase
    {
        ISpreadItRepository _repository;
        CommentReportFactory _commentReportFactory = new CommentReportFactory();
        readonly LinkGenerator _linkGenerator;

        public CommentReportsController(LinkGenerator linkGenerator)
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
                    var commentReports = _repository.GetCommentReport(id.Value);
                    if (commentReports == null)
                        return NotFound();
                    else
                        return Ok(_commentReportFactory.CreateCommentReport(commentReports));
                }
                else
                {
                    List<string> lstOfFields = new List<string>();
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var commentReports = _repository.GetCommentReports()
                                    .ApplySort(sort);

                    var totalCount = commentReports.Count();
                    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);


                    string prevLink = page > 1 ? _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "CommentReports",
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
                        controller: "CommentReports",
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


                    return Ok(commentReports
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize).ToList()
                        .Select(a => _commentReportFactory.CreateDataShapedObject(a, lstOfFields)));
                }
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "GetCommentReport"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]DTO.CommentReport commentReport)
        {
            try
            {
                if (commentReport != null && !commentReport.CommentId.Equals(0) && !commentReport.ReportCategoryId.Equals(0))
                {
                    var _commentReport = _commentReportFactory.CreateCommentReport(commentReport);
                    var result = _repository.InsertCommentReport(_commentReport);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newPostReport = _commentReportFactory.CreateCommentReport(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "CommentReports",
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
                    Method = "PostCommentReport"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            try
            {
                var result = _repository.ChangeCommentReportStatus(id);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    var UpdatedCommentReport = _commentReportFactory.CreateCommentReport(result.Entity);
                    var newCommentLink = _linkGenerator.GetPathByAction(
                    HttpContext,
                    action: "Get",
                    controller: "PostReports",
                    values: new
                    {
                        id = UpdatedCommentReport.Id
                    });

                    return Created(newCommentLink, UpdatedCommentReport);
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
                    Method = "PutCommentReport"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
