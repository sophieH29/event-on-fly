using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using EventOnFly.DataAccess.Data;
using EventOnFly.DataAccess.Data.DbModels;
using EventOnFly.Web.Auth;
using EventOnFly.Web.Auth.Models;
using EventOnFly.Web.Auth.Helpers;
using EventOnFly.Web.Auth.ViewModels;

namespace EventOnFly.Web.Controllers
{
  [Route("api/[controller]/[action]")]
  public class ExternalAuthController : Controller
  {

    private readonly EofDbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly FacebookAuthSettings _fbAuthSettings;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;
    private static readonly HttpClient Client = new HttpClient();
    public ExternalAuthController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor,
                                    UserManager<AppUser> userManager,
                                    EofDbContext appDbContext,
                                    IJwtFactory jwtFactory,
                                    IOptions<JwtIssuerOptions> jwtOptions)
    {
      _fbAuthSettings = fbAuthSettingsAccessor.Value;
      _userManager = userManager;
      _appDbContext = appDbContext;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
    }

    // POST api/externalauth/facebook
    [HttpPost]
    public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
    {
      // generate an app access token
      var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
      var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
      // validate the user access token
      var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
      var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

      if (!userAccessTokenValidation.Data.IsValid)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
      }

      // we've got a valid token so we can request user data from fb
      var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
      var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

      // ready to create the local user account (if necessary) and jwt
      var user = await _userManager.FindByEmailAsync(userInfo.Email);

      if (user == null)
      {
        var appUser = new AppUser
        {
          FirstName = userInfo.FirstName,
          LastName = userInfo.LastName,
          FacebookId = userInfo.Id,
          Email = userInfo.Email,
          UserName = userInfo.Email,
          PictureUrl = userInfo.Picture.Data.Url
        };

        var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

        if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

        await _appDbContext.Vendors.AddAsync(new Vendor { IdentityId = appUser.Id });
        await _appDbContext.SaveChangesAsync();
      }

      // generate the jwt for the local user...
      var localUser = await _userManager.FindByNameAsync(userInfo.Email);

      if (localUser == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
      }

      var jwt = await Tokens.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id, Constants.Strings.JwtClaims.VendorAccess),
        _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

      return new OkObjectResult(jwt);
    }
  }
}