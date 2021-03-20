using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeWithMukesh.Auth
{
    public partial class AppIdentityDbContext : IdentityDbContext <ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnApplicationUserCreating(modelBuilder);
        }

        partial void OnApplicationUserCreating(ModelBuilder modelBuilder);
    }
}