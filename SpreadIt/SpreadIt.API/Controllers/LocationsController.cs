using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;

namespace SpreadIt.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        ISpreadItRepository _repository;
        LocationFactory _locationFactory = new LocationFactory();
        readonly LinkGenerator _linkGenerator;

        public LocationsController(LinkGenerator linkGenerator)
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
                    var location = _repository.GetLocation(id.Value);
                    if (location == null)
                        return NotFound();
                    else
                        return Ok(_locationFactory.CreateLocation(location));
                }
                else
                    return Ok(_repository.GetLocations()
                        .Select(a => _locationFactory.CreateLocation(a)));
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "GetLocation"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] DTO.Location location)
        {
            try
            {
                if (location != null)
                {
                    var loc = _locationFactory.CreateLocation(location);
                    var result = _repository.InsertLocation(loc);

                    if (result.Status == RepositoryActionStatus.Created)
                    {
                        var newlocation = _locationFactory.CreateLocation(result.Entity);
                        var newLocationLink = _linkGenerator.GetPathByAction(
                            HttpContext,
                            action: "Get",
                            controller: "Locations",
                            values: new
                            {
                                id = newlocation.Id
                            });

                        return Created(newLocationLink, newlocation);
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
                    Method = "PostLocation"
                });

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteLocation(id);

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
                    Method = "DeleteLocation"
                });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}