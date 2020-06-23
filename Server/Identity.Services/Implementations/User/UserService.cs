namespace Identity.Services.Implementations.User
{
    using Contracts.User;
    using Data;
    using Helpers;
    using Identity.Data.Models.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models.Facebook;
    using Models.Facebook.ApiResponses;
    using Models.User;
    using Newtonsoft.Json;
    using StoreApi;
    using StoreApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private static readonly HttpClient Client = new HttpClient();

        private readonly ILogger<UserService> logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppSettings appSettings;

        public UserService(ILogger<UserService> logger,
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            this.logger = logger;
            this.db = db;
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        public async Task<QueryResult> Login(UserInputModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user == null || !(await this.userManager.CheckPasswordAsync(user, model.Password)))
            {
                this.logger.LogInformation($"Invalid email or password for user '{model.Email}'.");

                IdentityError[] errors = new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code = "CredentialsError",
                        Description = "Invalid email or password"
                    },
                };
                
                return QueryResult.Failed(errors);
            }

            this.logger.LogInformation($"User '{model.Email}' successfully logged in.");

            var token = await Tokens.GenerateJwtToken(user, this.userManager, this.appSettings);

            var result = QueryResult<TokenModel>.Suceeded(token);
            
            return result;
        }

        public async Task<QueryResult> Register(UserInputModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var createUserResult = await this.userManager.CreateAsync(user, model.Password);

            if (!createUserResult.Succeeded)
            {
                this.logger.LogInformation($"Failed user creation for '{model.Email}'.");

                return QueryResult.Failed(createUserResult.Errors.ToArray());
            }

            await this.userManager.AddToRoleAsync(user, WebConstants.UserRole);

            this.logger.LogInformation($"User '{model.Email}' with role {WebConstants.UserRole} created a new account.");

            var token = await Tokens.GenerateJwtToken(user, this.userManager, this.appSettings);

            var result = QueryResult<TokenModel>.Suceeded(token);

            return result;
        }

        public async Task<QueryResult> LoginWithFacebook(FacebookAuthViewModel model)
        {
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={this.appSettings.FbAppId}&client_secret={this.appSettings.FbAppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                this.logger.LogInformation("Invalid facebook token for.");

                IdentityError[] errors = new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code = "InvalidToken",
                        Description = "Invalid facebook token"
                    },
                };

                return QueryResult.Failed(errors);
            }

            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v5.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            var user = await this.userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                };

                var createUserResult = await this.userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!createUserResult.Succeeded)
                {
                    this.logger.LogInformation($"Failed user creation for '{appUser.Email}'.");

                    return QueryResult.Failed(createUserResult.Errors.ToArray());
                }

                await this.userManager.AddClaimsAsync(appUser,
                    new List<Claim>()
                {
                    new Claim("FirstName", userInfo.FirstName),
                    new Claim("LastName", userInfo.LastName),
                    new Claim("FacebookId", userInfo.Id.ToString()),
                    new Claim("PictureUrl1", userInfo.Picture.Data.Url)
                });

                this.logger.LogInformation($"User '{appUser.Email}' with role {WebConstants.UserRole} created a new account.");

                //await this.appDbContext.Customers.AddAsync(new Customer { IdentityId = appUser.Id, Location = "", Locale = userInfo.Locale, Gender = userInfo.Gender });
                //await this.appDbContext.SaveChangesAsync();
            }

            var localUser = await this.userManager.FindByEmailAsync(userInfo.Email);

            if (localUser == null)
            {
                this.logger.LogInformation("Failed to create local user account.");

                IdentityError[] errors = new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code = "LocalAccountCreationFailed",
                        Description = "Failed to create local user account"
                    },
                };

                return QueryResult.Failed(errors);
            }

            var token = await Tokens.GenerateJwtToken(user, this.userManager, this.appSettings);

            var result = QueryResult<TokenModel>.Suceeded(token);

            return result;
        }

        public IEnumerable<UserServiceModel> Users()
            => this.db
                .Users
                .Select(u => new UserServiceModel
                {
                    Email = u.Email
                })
                .ToList();
    }
}
