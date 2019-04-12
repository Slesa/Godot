using FluentNHibernate.Cfg.Db;
using PersonalPlanung.Persistence.nh.Contracts;

namespace Hibernate.Specs.Common
{
    public class SqLiteInFileDatabaseConfiguration : IPersistenceConfiguration 
    {
        public IPersistenceConfigurer GetConfiguration()
        {
            return SQLiteConfiguration
                .Standard
                .UsingFile("testonly.db")
                .ShowSql();
        }
    }
}