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
            _userStore = new UserStore<ApplicationUser>(new IdentityContext());
            _repository = new SpreadItRepository(
               new SpreadItContext());
        }

        public async Task<IActionResult> GetAsync(string userName, string sort = "UserName",
            string fields = null,
            int page = 1, int pageSize = 5)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var user = await _userManager.FindByNameAsync(userName);
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
        public async Task<IActionResult> PostAsync([FromBody] DTO.Account account)
        {
            try
            {
                var user = _accountFactory.CreateAccount(account);

                IdentityResult result = await _userManager.CreateAsync(user, account.Password);

                if (result.Succeeded)
                {
                    var newAccountLink = _linkGenerator.GetPathByAction(
                        HttpContext,
                        action: "Get",
                        controller: "Accounts",
                        values: new
                        {
                            userName = account.UserName
                        });

                    return Created(newAccountLink, user);
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
    }
}