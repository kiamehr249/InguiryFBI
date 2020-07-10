using FbiInquiry.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FbiInquiry.CivilRegistry.Service
{
    public interface IPersonService : IProjectService<Person>
    {
        Person Create();
    }

    public class PersonService : ProjectService<Person>, IPersonService
    {

        public PersonService(ICivilRegUnitOfWork uow) : base(uow)
        {
        }

        public Person Create()
        {
            return new Person();
        }

        public override IList<Person> GetPartOptional(List<Expression<Func<Person, bool>>> predicate, int startIndex, int pageSize)
        {
            var query = TEntity.Where(predicate[0]);
            for (int i = 1; i < predicate.Count; i++)
            {
                query = query.Where(predicate[i]);
            }
            return query.OrderBy(i => i.Id).ThenBy(t => t.Id).Skip(startIndex).Take(pageSize).ToList();
        }
    }
}