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
            .AddDbContext<AuthDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("IdentityDbConnection")));
                        
            services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}