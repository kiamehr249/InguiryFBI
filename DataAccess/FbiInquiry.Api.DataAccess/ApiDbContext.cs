using Microsoft.EntityFrameworkCore;

namespace FbiInquiry.Api.DataAccess
{
    public class ApiDbContext : DbContext
    {
        private string conString;
        public ApiDbContext(string connectionString) : base()
        {
            conString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString);
        }

        public DbSet<SourceApi> SourceApis { get; set; }
        public DbSet<ApiUsage> ApiUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SourceApiConfig());
            builder.ApplyConfiguration(new ApiUsageConfig());
        }


        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

    }
}