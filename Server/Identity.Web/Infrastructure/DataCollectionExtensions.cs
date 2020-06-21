namespace Identity.Web.Infrastructure
{
    using Data.Models.Users;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using StoreApi;
    using System;
    using System.Threading.Tasks;

    public static class DataCollectionExtensions
    {
        public static async Task UseDataSeed(this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = new[]
            {
                WebConstants.AdministratorRole,
                WebConstants.EditorRole,
                WebConstants.UserRole
            };

            IdentityResult roleResult;
            foreach (var role in roles)
            {
                var roleCheck = await roleManager.RoleExistsAsync(role);
                if (!roleCheck)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = configuration.GetValue<string>("AdminCredentials:Email");
            var adminPassword = configuration.GetValue<string>("AdminCredentials:Password");

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };

                await userManager.CreateAsync(adminUser, adminPassword);

                await userManager.AddToRolesAsync(adminUser,
                    new[] { WebConstants.UserRole, WebConstants.AdministratorRole });
            }
        }
    }
}
