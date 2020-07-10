using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FbiInquiry.DataModel
{
    public class ProjectContext : DbContext, IUnitOfWork
    {
        private string conString;
        public ProjectContext() : base()
        {
        }
        public ProjectContext(string connectionString) : base()
        {
            conString = connectionString;
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public DatabaseFacade  DataFace
        {
            get { return this.Database; }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await base.SaveChangesAsync();
        }

        public int SaveChange()
        {
            return base.SaveChanges();
        }

    }
}
