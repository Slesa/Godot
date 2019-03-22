using System.Linq;

namespace ddd.core
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Save<T>(T entity);
        T Load<T>(ulong id);
        IQueryable<T> Query<T>();
        //IQueryable<T> Query<T>(IDomainQuery<T> whereQuery);
    }
}