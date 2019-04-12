using NHibernate;

namespace PersonalPlanung.Persistence.nh.Contracts
{
    public interface IDomainQuery<out TResult>
    {
        TResult Execute(ISession session);
    }
}