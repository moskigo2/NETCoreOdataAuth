using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreTestApp.Model;
using CoreTestApp.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly TestAppDbContext context;
        private SignInManager<Worker> _signManager;
        private UserManager<Worker> _userManager;

        public WorkerController(TestAppDbContext context, UserManager<Worker> userManager, SignInManager<Worker> signManager)
        {
            this.context = context;
            _signManager = signManager;
            _userManager = userManager;
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            return Ok();
        }

        [HttpGet("oall")]
        [EnableQuery]
        public IActionResult GetODataWorkers()
        {
            return Ok( context.Workers);
             //return Ok(context.Workers.Select(a=> new { a.Name, a.CompanyId, a.Company, a.Email, a.RowVersion}));
        }


        [HttpGet("all"), Authorize]
        public IActionResult GetWorkers()
        {
            var workers = _userManager.Users.Select(a => new { a.Name, a.Email, a.Company.CompanyName, a.RowVersion, a.ConcurrencyStamp });
            return Ok(workers);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddWorker(Worker worker)
        {
            var user = new Worker
            {
                UserName = worker.Name.Split(" ")[0],
                Name = worker.Name,
                CompanyId = worker.CompanyId,
                Email = worker.Email,
            };

            var result = await _userManager.CreateAsync(user, worker.Password);

            return Ok(result.Succeeded);

        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn (WorkerLogin worker)
        {
            var res = await _signManager.PasswordSignInAsync(worker.Login, worker.Password, true, false);

            if (res.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Login or password invalid");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
             await _signManager.SignOutAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Worker worker)
        {
            //context.Entry(worker).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //var res = await context.SaveChangesAsync();
            var res = await _userManager.UpdateAsync(worker);
                
                return Ok(res);
           
        }
    }
}
