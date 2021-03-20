using Microsoft.EntityFrameworkCore;

namespace CodeWithMukesh.Auth
{
    public partial class AppIdentityDbContext
    {
        partial void OnApplicationUserCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        }
    }
}