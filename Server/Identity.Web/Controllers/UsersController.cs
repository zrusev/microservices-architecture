namespace Identity.Web.Controllers
{
    using Data.Models.Users;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;
    using Models.Users;
    using Services.Contracts.User;
    using StoreApi;
    using StoreApi.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : ApplicationController
    {
        private readonly ILogger<UsersController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppSettings appSettings;
        private readonly IUserService userService;

        public UsersController(ILogger<UsersController> logger,
                               UserManager<ApplicationUser> userManager, 
                               SignInManager<ApplicationUser> signInManager,
                               IOptions<AppSettings> appSettings,
                               IUserService userService)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, WebConstants.UserRole);

                this.logger.LogInformation($"User '{model.Email}' with role {WebConstants.UserRole} created a new account.");

                await this.signInManager.SignInAsync(user, false);

                return Ok(await Tokens.GenerateJwtToken(user, this.userManager, this.appSettings));
            }

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user == null || !(await this.userManager.CheckPasswordAsync(user, model.Password)))
            {
                this.logger.LogInformation($"Invalid email or password for user '{model.Email}'.");

                return BadRequest(new
                { 
                    errors = new IdentityError[]
                    {
                        new IdentityError()
                        {
                            Code = "CredentialsError",
                            Description = "Invalid email or password"
                        },
                    }               
                });
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (!result.Succeeded)
                return Unauthorized();

            this.logger.LogInformation($"User '{model.Email}' successfully logged in.");

            return Ok(await Tokens.GenerateJwtToken(user, this.userManager, this.appSettings));
        }

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