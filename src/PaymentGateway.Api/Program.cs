using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using PaymentGateway.Api.Extensions;
using System;
using System.Reflection;

namespace PaymentGateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
            return Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                       if (!isDevelopment)
                       {
                           webBuilder.UseKestrel(options => options.ConfigureEndpoints());
                       }

                   }).ConfigureLogging(logging =>
                   {
                       logging.ClearProviders();
                       logging.SetMinimumLevel(LogLevel.Trace);
                       logging.AddConsole();
                       logging.AddDebug();
                   })
                    .UseNLog()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {

                        config.AddJsonFile("appsettings.json", optional: true);
                        var appAssembly = Assembly.GetExecutingAssembly();
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }

                    }).UseWindowsService();
        }
    }
}
