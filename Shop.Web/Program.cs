using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Shop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builtConfig = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddCommandLine(args)
               .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(builtConfig["Logging:FilePath"])
                .CreateLogger();
            return Host.CreateDefaultBuilder(args)
                 .ConfigureLogging(logging =>
                 {
                     logging.AddSerilog();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
