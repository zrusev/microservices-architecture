namespace Identity.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using StoreApi.Helpers;

    public class Program
    {
        public static void Main(string[] args)
            => Logger.Register(CreateHostBuilder(args));

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host
                .CreateDefaultBuilder(args)
                .UseLogger()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}