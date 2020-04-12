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
        public IActionResult GetCommentsReports(int? CommentId, string sort = "CreatedDate", string fields = null,
            int page = 1, int pageSize = 5)
        {
            try
            {
                if (CommentId.HasValue)
                    return Ok(_repository.GetCommentReportsByCommentId(CommentId.Value).
                        Select(element => _commentReportFactory.CreateCommentReport(element)));
                else
                {
                    List<string> lstOfFields = new List<string>();
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var postReports = _repository.GetCommentReports()
                                    .ApplySort(sort)
                                    .Select(a => _commentReportFactory.CreateDataShapedObject(a, lstOfFields));

                    var totalCount = postReports.Count();
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


                    return Ok(postReports
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
        public IActionResult CreateCommentReport([FromBody]DTO.CommentReport commentReport)
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
                        action: "Post",
                        controller: "PostReports",
                        values: new
                        {
                            id = newPostReport.Id
                        });

                        //return Created(newCommentLink, newComment);
                        return Created("New Post Report Has been Created", newPostReport);
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
                    Method = "Post"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ChangeCommentReportStatus(int id)
        {
            try
            {
                if (!id.Equals(0))
                {
                    var result = _repository.ChangeCommentReportStatus(id);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var UpdatedCommentReport = _commentReportFactory.CreateCommentReport(result.Entity);
                        var newCommentLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Put",
                        controller: "PostReports",
                        values: new
                        {
                            id = UpdatedCommentReport.Id
                        });

                        //return Created(newCommentLink, newComment);
                        return Created("Comment Report Has been Updated", UpdatedCommentReport);
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
                    Method = "Post"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
