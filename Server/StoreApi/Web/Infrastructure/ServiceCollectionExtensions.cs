namespace StoreApi.Web.Infrastructure
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using StoreApi.Services.Contracts.Services;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConventionalServices(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);

            var entryAssembly = Assembly.GetEntryAssembly().GetName();

            var commonTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(n => n.GetName().Name == "StoreApi")
                .GetExportedTypes();

            var serviceTypes = Assembly
                .Load(entryAssembly.Name.Replace("Web", "Services"))
                .GetExportedTypes();

            var types = commonTypes
                .Concat(serviceTypes)
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (serviceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
                else if (singletonServiceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            }

            return services;
        }

        public static IServiceCollection AddMappingServices(this IServiceCollection services)
            => services
                .AddAutoMapper((_, config) => config
                                    .AddProfile(new ConventionalMappingProfile()),
                                Array.Empty<Assembly>());
        
        public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
            => services
                    .AddScoped<DbContext, TDbContext>()
                    .AddDbContext<TDbContext>(options => options
                        .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        public static IServiceCollection AddTokenHandler(this IServiceCollection services, IConfigurationSection appSettingsSection)
        {
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.Issuer,
                        ValidateIssuer = false,
                        ValidAudience = appSettings.Audience,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

            services.AddHttpContextAccessor();
            return services;
        }
    }
}   