using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FbiInquiry.Api.DataAccess
{
    public class SourceApiConfig : IEntityTypeConfiguration<SourceApi>
    {
        public void Configure(EntityTypeBuilder<SourceApi> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("SourceApis");
        }
    }
}
