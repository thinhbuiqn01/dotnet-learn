using System.Security.Claims;

namespace api.Extension
{

  public static class ClaimsExtension
  {

    public static string? GetUserName(this ClaimsPrincipal principal)
    {

      //http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname
      return principal.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.GivenName))?.Value;
      // return principal.Claims.SingleOrDefault(x => true)?.Value;

    }
  }
}