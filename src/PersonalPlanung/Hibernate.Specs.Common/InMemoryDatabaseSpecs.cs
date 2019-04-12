using System.Data.SQLite;
using System.Diagnostics;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using Machine.Specifications;
using NHibernate;

namespace Hibernate.Specs.Common
{
    [Subject(typeof(SQLiteConfiguration))]
    public class InMemoryDatabaseSpecs<TMappingAssembly>
    {
        Establish context = () =>
        {
            //NHibernateProfiler.Initialize();

            var forceSqlLiteReference = typeof(SQLiteException) != null;
            Trace.Assert(forceSqlLiteReference);
            Debug.Assert(forceSqlLiteReference);

            var configuration = Fluently.Configure()
                .Database(new SqLiteInMemoryDatabaseConfiguration().GetConfiguration())
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<TMappingAssembly>());

            var source = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);
            source.BuildSchema();

            SessionFactory = source.SessionFactory;
            Session = source.CreateSession();
        };

        Cleanup teardown = () =>
        {
            Session?.Close();
        };

        protected static ISessionFactory SessionFactory { get; private set; }
        protected static ISession Session { get; set; }
    }
}