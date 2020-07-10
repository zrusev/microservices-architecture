namespace StoreApi.Web.Infrastructure
{
    using Microsoft.AspNetCore.Authorization;

    using static WebConstants;
    
    public class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAttribute() => this.Roles = AdministratorRole;
    }
}
