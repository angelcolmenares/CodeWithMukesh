using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeWithMukesh.Auth
{
    public static class AuthServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("AppIdentityDbConnection")));
                        
            services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}