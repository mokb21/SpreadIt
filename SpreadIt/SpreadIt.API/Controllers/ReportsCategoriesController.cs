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
        public IActionResult GetReportsCategories(int? id)
        {
            try
            {
                if (id.HasValue)
                    return Ok(_reportsCategoriesFactory.CreateReportCategory(
                        _repository.GetReportCategoryById(id.Value)));
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
                    Method = "Get"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
