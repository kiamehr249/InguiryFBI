using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FbiInquiry.DataAccess
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
            builder.ToTable("Roles");
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
        }
    }
}
