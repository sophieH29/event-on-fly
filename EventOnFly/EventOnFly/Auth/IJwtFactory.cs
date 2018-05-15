using System.Security.Claims;
using System.Threading.Tasks;

namespace EventOnFly.Web.Auth
{
  /// <summary>
  /// Helper to create the encoded tokens to exchange between the client and backend
  /// </summary>
  public interface IJwtFactory
  {
    Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
    ClaimsIdentity GenerateClaimsIdentity(string userName, string id, string accessClaim = Helpers.Constants.Strings.JwtClaims.ApiAccess);
  }
}
