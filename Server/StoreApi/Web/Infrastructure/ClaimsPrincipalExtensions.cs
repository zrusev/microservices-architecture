namespace StoreApi.Web.Infrastructure
{
    using System.Security.Claims;
 
    using static WebConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdministrator(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRole);
    }
}