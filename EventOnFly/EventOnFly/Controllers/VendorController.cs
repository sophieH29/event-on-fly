using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using EventOnFly.DataAccess.Data;
using EventOnFly.DataAccess.Data.DbModels;
using EventOnFly.Web.Auth.ViewModels;
using EventOnFly.Web.Auth.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EventOnFly.Web.Controllers
{
  [Route("api/[controller]")]
  public class VendorController : Controller
  {
    private readonly EofDbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ClaimsPrincipal _caller;

    public VendorController(UserManager<AppUser> userManager, IMapper mapper, EofDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
      _userManager = userManager;
      _mapper = mapper;
      _appDbContext = appDbContext;
      _caller = httpContextAccessor.HttpContext.User;
    }

    // POST api/vendor/register
    [HttpPost("register")]
    public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userIdentity = _mapper.Map<AppUser>(model);
      userIdentity.DateCreated = DateTime.Now;
      userIdentity.DateUpdated = DateTime.Now;
      userIdentity.LastLogin = DateTime.Now;

      var result = await _userManager.CreateAsync(userIdentity, model.Password);

      if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

      await _appDbContext.Vendors.AddAsync(new Vendor { IdentityId = userIdentity.Id});
      await _appDbContext.SaveChangesAsync();

      return new OkObjectResult("Account created");
    }

    // GET api/vendor/home
    [Authorize(Policy = "VendorUser")]
    [HttpGet("home")]
    public async Task<IActionResult> Home()
    {
      // retrieve the user info
      //HttpContext.User
      var userId = _caller.Claims.Single(c => c.Type == "id");
      var vendor = await _appDbContext.Vendors.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

      return new OkObjectResult(new
      {
        Message = "This is secure API and user data!",
        vendor.Identity.FirstName,
        vendor.Identity.LastName,
        vendor.Identity.PictureUrl,
        vendor.Identity.FacebookId,
        vendor.Identity.DateCreated
      });
    }
  }
}