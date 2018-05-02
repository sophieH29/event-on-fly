using EventOnFly.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventOnFly.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        //[HttpPost("[action]")]
        //public IActionResult PostTest([FromBody]Venue venue)
        //{
        //    context.Venues.Add(venue);
        //    context.SaveChanges();
        //    return Ok();
        //}

        //[HttpGet("[action]")]
        //public IEnumerable<Venue> GetAllTestModels()
        //{
        //    var models = context.Venues.ToList();
        //    return models;
        //}
    }
}
