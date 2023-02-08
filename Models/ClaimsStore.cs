using System.Security.Claims;

namespace OurProject.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("List Role","List Role"),
            new Claim("Create Role","Create Role"),
            new Claim("Delete Role","Delete Role")
    };

    }
}
