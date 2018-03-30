using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOnFly.Data;
using EventOnFly.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventOnFly.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly EofDbContext context;

        public SampleDataController(EofDbContext context)
        {
            this.context = context;
        }

        [HttpPost("[action]")]
        public IActionResult PostTest([FromBody]TestModel model)
        {
            model = new TestModel { DateTimeColumn = model.DateTimeColumn, StringColumn = model.StringColumn };
            context.TestModels.Add(model);
            context.SaveChanges();
            return Ok();
        }

        [HttpGet("[action]")]
        public IEnumerable<TestModel> GetAllTestModels()
        {
            var models = context.TestModels.ToList();
            return models;
        }
    }
}
