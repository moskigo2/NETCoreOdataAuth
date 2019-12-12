using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTestApp.Model;
using CoreTestApp.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
       private readonly TestAppDbContext context;

        public CompanyController(TestAppDbContext context)
        {
            this.context = context;
        }

        [EnableQuery]
        [HttpGet("all")]
        public IEnumerable<Company> GetCompany()
        {
            return context.Companies;
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            var company = context.Companies.Find(id);

            if(company != null)
            {
                return Ok(company);
            }

            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany (Company company)
        {
            context.Companies.Add(company);

            var res = await context.SaveChangesAsync();

            return Ok(res);
        }
    }
}
