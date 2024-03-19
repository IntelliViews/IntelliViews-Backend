using System.Security.Claims;
using System.Security.Principal;

namespace IntelliViews.API.Helpers
{
    public static class ClaimPrincipalHelper
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            IEnumerable<Claim> claims = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier);
            return claims.Count() >= 2 ? claims.ElementAt(1).Value : null;

        }


        public static string? Email(this ClaimsPrincipal user)
        {
            Claim? claim = user.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

       
    }
}
