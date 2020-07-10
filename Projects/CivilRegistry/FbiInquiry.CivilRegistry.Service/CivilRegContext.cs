using FbiInquiry.DataModel;
using Microsoft.EntityFrameworkCore;

namespace FbiInquiry.CivilRegistry.Service
{
    public class CivilRegContext : ProjectContext, ICivilRegUnitOfWork
    {
        private string conString;
        public CivilRegContext(string connectionString) : base(connectionString)
        {
            conString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString);
        }

        DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonConfig());
        }


    }
}
