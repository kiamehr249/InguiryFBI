using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FbiInquiry.DataModel
{
    public interface IProjectService<T> : IDisposable where T : class
    {
        void Add(T entity);

        void Remove(T entity);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        T Find(Expression<Func<T, bool>> predicate);

        IList<T> GetPartOptional(List<Expression<Func<T, bool>>> predicate, int startIndex, int size);

        IList<T> GetPart(Expression<Func<T, bool>> predicate, int startIndex, int size);

        List<Expression<Func<T, bool>>> ExpressionMaker();

        int ExecSqlCommand(string command);
        int ExecSqlCommand(string command, object[] values);

        Task<int> SaveChangesAsync();

        int SaveChanges();

    }
}
