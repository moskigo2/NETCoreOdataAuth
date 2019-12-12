using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTestApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreTestApp.Controllers
{
    [Route("api/[controller]"), Authorize("admin")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private UserManager<Worker> UserManager;
        private RoleManager<IdentityRole> RoleManager;

        public RoleController(UserManager<Worker> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var role = await UserManager.GetRolesAsync(user);
            return Ok(new { roles = RoleManager.Roles, CurrentUserRole = role });
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateRole ([FromBody]string roleName)
        {
            var role = new IdentityRole(roleName);

            await RoleManager.CreateAsync(role);

            return Ok();
        }


        [HttpPost("include")]
        public async Task<IActionResult> AddToRole(WorkerRole item)
        {
            var user = await UserManager.FindByIdAsync(item.UserId);
            var role = await RoleManager.FindByIdAsync(item.RoleId);

            if(user == null || role == null)
            {
                return BadRequest("User or role not found");
            }

            var res = await UserManager.AddToRoleAsync(user, role.Name);

            return Ok(res.Succeeded);
        }
    }
}
