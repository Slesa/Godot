using NHibernate;
using NHibernate.Cfg;

namespace PersonalPlanung.Persistence.nh.Contracts
{
    public interface IHibernateInitializationAware
    {
        void Initialized(Configuration configuration, ISessionFactory sessionFactory);
    }
}