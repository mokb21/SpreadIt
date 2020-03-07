using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpreadIt.Constants;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        ISpreadItRepository _repository;
        LocationFactory _locationFactory = new LocationFactory();

        public LocationsController()
        {
            _repository = new SpreadItRepository(
                new Repository.Models.SpreadItContext());
        }

        [HttpGet]
        public IActionResult Get(int? id)
        {
            try
            {
                if (id.HasValue)
                    return Ok(_locationFactory.CreateLocation(
                        _repository.GetLocation(id.Value)));
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
                    Method = "Get"
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
                        return Created("api/Location?id=" + newlocation.Id.ToString(),
                            newlocation);
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
                    Method = "Delete"
                });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}