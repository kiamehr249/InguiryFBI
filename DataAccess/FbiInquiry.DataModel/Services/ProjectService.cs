using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FbiInquiry.DataModel
{
    public class ProjectService<T> : IProjectService<T> where T : class
    {
        protected IUnitOfWork _uow;
        protected DbSet<T> TEntity;

        protected ProjectService(IUnitOfWork uow)
        {
            _uow = uow;
            TEntity = _uow.Set<T>();
        }

        public virtual void Add(T entity)
        {
            TEntity.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            TEntity.Remove(entity);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await TEntity.CountAsync(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return TEntity.Count(predicate);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await TEntity.Where(predicate).FirstOrDefaultAsync();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return TEntity.Where(predicate).FirstOrDefault();
        }

        public virtual IList<T> GetPartOptional(List<Expression<Func<T, bool>>> predicate, int startIndex, int size)
        {
            var query = TEntity.Where(predicate[0]);
            for (int i = 1; i < predicate.Count; i++)
            {
                query = query.Where(predicate[i]);
            }
            return query.Skip(startIndex).Take(size).AsNoTracking().ToList();
        }

        public virtual IList<T> GetPart(Expression<Func<T, bool>> predicate, int startIndex, int size)
        {
            return TEntity.Where(predicate).Skip(startIndex).Take(size).AsNoTracking().ToList();
        }


        public List<Expression<Func<T, bool>>> ExpressionMaker()
        {
            return new List<Expression<Func<T, bool>>>();
        }

        public int ExecSqlCommand(string command)
        {
            return _uow.DataFace.ExecuteSqlCommand(command);
        }

        public int ExecSqlCommand(string command, object[] values)
        {
            return  _uow.DataFace.ExecuteSqlCommand(command, values);
        }

        public List<T> GetDataFromSp(string command, object[] values)
        {
            return TEntity.FromSqlRaw<T>(command, values).ToList(); 
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _uow.SaveChangeAsync();
        }

        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
