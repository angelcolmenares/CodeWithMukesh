using System;
using System.Threading.Tasks;
using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeWithMukesh
{
    public class Program
    {
        public  static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                try
                {                    
                    await ContextSeed.SeedRolesAsync(services); 
                    logger.LogInformation("Seed roles ok");
                         
                    await ContextSeed.SeedSuperAdminAsync(services);
                    logger.LogInformation("Seed super admin ok");
                }
                catch (Exception ex)
                {
                    
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
