using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpreadIt.Repository;
using SpreadIt.Repository.Factories;
using SpreadIt.Repository.Models;

namespace SpreadIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        UserStore<ApplicationUser> _userStore;
        readonly LinkGenerator _linkGenerator;
        ISpreadItRepository _repository;

        AccountFactory _accountFactory = new AccountFactory();

        public LoginController(UserManager<ApplicationUser> userManager, LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _userStore = new UserStore<ApplicationUser>(new SpreadItContext());
            _linkGenerator = linkGenerator;
            _repository = new SpreadItRepository(
                new SpreadItContext());
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> IndexAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return NotFound();
            else
            {
                user.UserLocations = _repository.GetUserLocations(user.Id);
                return Ok(_accountFactory.CreateAccount(user));
            }
        }
    }
}