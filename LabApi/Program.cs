using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LabApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var configuration = Configuration(environmentName);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Environment", environmentName)
                .CreateLogger();

            try
            {
                Log.Information("Iniciando o web host");
                CreateHostBuilder(args, configuration).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host encerrado de forma inesperada");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
           Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((host, cfg) => cfg.AddConfiguration(configuration))
            // .ConfigureHostConfiguration((cfg) => cfg.AddConfiguration(Configuration(environmentName)))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        public static IConfiguration Configuration(string environmentName) =>
            new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
    }
}