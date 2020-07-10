using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FbiInquiry.DataAccess
{
    public class AppDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new UserClaimConfig());
            builder.ApplyConfiguration(new UserLoginConfig());
            builder.ApplyConfiguration(new UserTokenConfig());
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new RoleClaimConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
        }


    }
}
