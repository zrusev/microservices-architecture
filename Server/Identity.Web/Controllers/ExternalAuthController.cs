namespace Identity.Web.Controllers
{
    using Identity.Services.Contracts.User;
    using Identity.Services.Models.Facebook;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ExternalAuthController : ApplicationController
    {
        private readonly IUserService userService;

        public ExternalAuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(Facebook))]
        public async Task<ActionResult> Facebook(FacebookAuthViewModel model)
            => QueryResultExtensions.ToActionResult(await (dynamic)this.userService.LoginWithFacebook(model));
    }
}