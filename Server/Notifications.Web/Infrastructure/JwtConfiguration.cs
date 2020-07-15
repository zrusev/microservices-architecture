namespace Notifications.Web.Infrastructure
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using System.Threading.Tasks;

    public class JwtConfiguration
    {
        public static JwtBearerEvents BearerEvents
            => new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/notifications"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
    }
}
