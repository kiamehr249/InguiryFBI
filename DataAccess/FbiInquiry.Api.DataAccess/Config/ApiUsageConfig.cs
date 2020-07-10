using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FbiInquiry.Api.DataAccess
{
    public class ApiUsageConfig : IEntityTypeConfiguration<ApiUsage>
    {
        public void Configure(EntityTypeBuilder<ApiUsage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ApiUsages");
            builder.HasOne(x => x.SourceApi)
                .WithMany(x => x.ApiUsages)
                .HasForeignKey(x => x.ApiId);


        }
    }
}
