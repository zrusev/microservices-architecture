namespace Identity.Web.Controllers
{
    using Data.Models.Users;
    using Identity.Services.Models.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Services.Contracts.User;
    using StoreApi;
    using StoreApi.Models;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : ApplicationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppSettings appSettings;
        private readonly IUserService userService;

        public UsersController(UserManager<ApplicationUser> userManager, 
                               SignInManager<ApplicationUser> signInManager,
                               IOptions<AppSettings> appSettings,
                               IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(UserInputModel model)
            => QueryResultExtensions.ToActionResult(await (dynamic)this.userService.Register(model));

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(UserInputModel model)
            => QueryResultExtensions.ToActionResult(await (dynamic)this.userService.Login(model));

        [HttpGet]
        [Authorize(Roles = WebConstants.AdministratorRole)]
        public ActionResult GetAll()
        {
            var claims = HttpContext.User.Claims
                .Select(c =>
                new 
                {
                    Type = c.Type,
                    Value = c.Value
                })
                .ToList();

            var users = this.userService.Users();
            
            return Ok(users);
        }
    }
}