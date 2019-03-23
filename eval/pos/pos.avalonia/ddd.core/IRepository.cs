using System.Linq;

namespace ddd.core
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Save(T entity);
        T Load(ulong id);
        IQueryable<T> Query();
        //IQueryable<T> Query<T>(IDomainQuery<T> whereQuery);
    }
}