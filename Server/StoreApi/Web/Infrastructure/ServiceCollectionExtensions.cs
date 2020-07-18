namespace StoreApi.Web.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using GreenPipes;
    using Hangfire;
    using MassTransit;
    using Messages;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
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
        
        public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services, string connection)
            where TDbContext : DbContext
            => services
                    .AddScoped<DbContext, TDbContext>()
                    .AddDbContext<TDbContext>(options => options
                        .UseSqlServer(connection, 
                            options => options
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)));
        
        public static IServiceCollection AddTokenHandler(this IServiceCollection services, 
            IConfigurationSection appSettingsSection, 
            JwtBearerEvents events = null)
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

                    if (events != null)
                    {
                        options.Events = events;
                    }
                });

            services.AddHttpContextAccessor();

            return services;
        }

        //public static IServiceCollection AddHealth(
        //    this IServiceCollection services,
        //    IConfiguration configuration)
        //{
        //    var healthChecks = services.AddHealthChecks();

        //    healthChecks
        //        .AddSqlServer(configuration.GetDefaultConnectionString());

        //    healthChecks
        //        .AddRabbitMQ(rabbitConnectionString: "amqp://rabbitmq:rabbitmq@rabbitmq/");

        //    return services;
        //}

        public static IServiceCollection AddMessaging(this IServiceCollection services,
            IConfigurationSection appSettingsSection,
            params Type[] consumers)
        {
            var settings = appSettingsSection.Get<MassTransitSettings>();

            services
                .AddMassTransit(mt =>
                {
                    consumers.ForEach(consumer => mt.AddConsumer(consumer));

                    mt.AddBus(bus => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(settings.HostName,
                            host =>
                            {
                                host.Username(settings.User);
                                host.Password(settings.Password);
                            });

                        consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName,
                            endpoint =>
                            {
                                endpoint.PrefetchCount = 6;
                                endpoint.UseMessageRetry(retry => retry.Interval(5, 100));

                                endpoint.ConfigureConsumer(bus, consumer);
                            }));
                    }));
                })
                .AddMassTransitHostedService();

            return services;
        }

        public static IServiceCollection AddMessagingWorker(this IServiceCollection services,
            string connection)
        {
            services
                .AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(connection));

            services.AddHangfireServer();

            services.AddHostedService<MessagesHostedService>();

            return services;
        }
    }
}   