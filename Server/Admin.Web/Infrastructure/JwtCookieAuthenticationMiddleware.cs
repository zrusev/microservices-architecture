namespace Admin.Web.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using StoreApi.Services.Contracts.Identity;
    using System.Threading.Tasks;

    using static StoreApi.InfrastructureConstants;

    public static class JwtCookieAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieAuthentication(
            this IApplicationBuilder app)
            => app
                .UseMiddleware<JwtCookieAuthenticationMiddleware>()
                .UseAuthentication();
    }

    public class JwtCookieAuthenticationMiddleware : IMiddleware
    {
        private readonly ICurrentTokenService currentToken;

        public JwtCookieAuthenticationMiddleware(ICurrentTokenService currentToken)
            => this.currentToken = currentToken;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Cookies[AuthenticationCookieName];

            if (token != null)
            {
                this.currentToken.Set(token);

                context.Request.Headers.Append(AuthorizationHeaderName, $"{AuthorizationHeaderValuePrefix} {token}");
            }

            await next.Invoke(context);
        }
    }
}
