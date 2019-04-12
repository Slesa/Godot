using FluentNHibernate.Cfg.Db;
using PersonalPlanung.Persistence.nh.Contracts;

namespace Hibernate.Specs.Common
{
    public class SqLiteInMemoryDatabaseConfiguration : IPersistenceConfiguration
    {
        public IPersistenceConfigurer GetConfiguration()
        {
            return SQLiteConfiguration
                .Standard
                .InMemory()
                .ShowSql();
        }
    }
}