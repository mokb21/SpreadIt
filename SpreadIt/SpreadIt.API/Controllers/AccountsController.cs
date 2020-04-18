using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpreadIt.API.Helpers;
using SpreadIt.Constants;
using SpreadIt.Repository.Models;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        UserStore<ApplicationUser> _userStore;
        ISpreadItRepository _repository;
        readonly LinkGenerator _linkGenerator;

        AccountFactory _accountFactory = new AccountFactory();


        public AccountsController(UserManager<ApplicationUser> userManager, LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
            _userManager = userManager;
            _userStore = new UserStore<ApplicationUser>(new SpreadItContext());
            _repository = new SpreadItRepository(
               new SpreadItContext());
        }

        public async Task<IActionResult> GetAsync(string id, string sort = "UserName",
            string fields = null,
            int page = 1, int pageSize = 5)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                        return NotFound();
                    else
                        return Ok(_accountFactory.CreateAccount(user));
                }
                else
                {
                    List<string> lstOfFields = new List<string>();
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var users = _userManager.Users;
                    users = users.ApplySort(sort);

                    var totalCount = users.Count();
                    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                    string prevLink = page > 1 ? _linkGenerator.GetPathByAction(
                       HttpContext,
                       action: "GetAsync",
                       controller: "Accounts",
                       values: new
                       {
                           page = page - 1,
                           pageSize = pageSize,
                           sort = sort,
                           fields = fields
                       }) : "";

                    var nextLink = page < totalPages ? _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "GetAsync",
                        controller: "Accounts",
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

                    return Ok(users
                            .Skip(pageSize * (page - 1))
                            .Take(pageSize).ToList()
                            .Select(a => _accountFactory.CreateDataShapedObject(a, lstOfFields)));
                }
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "GetAsyncAccount"
                });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] DTO.Account account)
        {
            try
            {
                //Adding User
                if (Request.Form.Files != null)
                {
                    var file = Request.Form.Files[0];

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(SpreadItConstants.UserImagesFolderPath, fileName);

                    account.Image = fullPath;

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                var user = _accountFactory.CreateAccount(account);
                IdentityResult result = await _userManager.CreateAsync(user, account.Password);

                //Adding UserLocations
                RepositoryActionStatus status = RepositoryActionStatus.Error;
                if (result.Succeeded)
                {
                    //Adding Image

                    var userAdded = await _userManager.FindByNameAsync(account.UserName);

                    if (userAdded != null)
                    {
                        List<UserLocation> userLocations = new List<UserLocation>();

                        var locations = JsonConvert.DeserializeObject<List<int>>(account.Locations);
                        userLocations.AddRange(locations.Select(a => new UserLocation() { LocationId = a, UserId = userAdded.Id }));
                        status = _repository.InsertUserLocation(userLocations);
                    }
                }

                if (status == RepositoryActionStatus.Created)
                {
                    var newAccountLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "Accounts",
                        values: new
                        {
                            id = user.Id
                        });

                    return Created(newAccountLink, _accountFactory.CreateAccount(user));
                }
                else
                {
                    return BadRequest(new { error = result.Errors.ToList() });
                }
            }
            catch (Exception ex)
            {
                _repository.InsertMessageLog(new Repository.Models.MessageLog
                {
                    Project = (byte)ProjectType.API,
                    Message = ex.Message,
                    Method = "PostAsyncAccount"
                });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = _userManager.DeleteAsync(user.Result);
                    if (result.Result.Succeeded)
                        return NoContent();
                    else
                        return NotFound();
                }
                return NotFound();
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