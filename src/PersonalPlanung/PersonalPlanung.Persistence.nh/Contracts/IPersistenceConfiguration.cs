using FluentNHibernate.Cfg.Db;

namespace PersonalPlanung.Persistence.nh.Contracts
{
    public interface IPersistenceConfiguration
    {
        IPersistenceConfigurer GetConfiguration();
    }
}