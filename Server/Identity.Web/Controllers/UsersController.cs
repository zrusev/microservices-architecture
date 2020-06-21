﻿namespace Identity.Web.Controllers
{
    using Data.Models.Users;
    using Helpers;
    using Identity.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;
    using Models.Users;
    using StoreApi;
    using StoreApi.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : ApplicationController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger,
                               UserManager<ApplicationUser> userManager, 
                               SignInManager<ApplicationUser> signInManager,
                               IOptions<AppSettings> appSettings,
                               IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
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

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, WebConstants.UserRole);

                _logger.LogInformation($"User '{model.Email}' with role {WebConstants.UserRole} created a new account.");

                await _signInManager.SignInAsync(user, false);

                return Ok(await Tokens.GenerateJwtToken(user, _userManager, _appSettings));
            }

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                _logger.LogInformation($"Invalid email or password for user '{model.Email}'.");

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

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (!result.Succeeded)
                return Unauthorized();

            _logger.LogInformation($"User '{model.Email}' successfully logged in.");

            return Ok(await Tokens.GenerateJwtToken(user, _userManager, _appSettings));
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

            var users = _userService.Users();
            
            return Ok(users);
        }
    }
}