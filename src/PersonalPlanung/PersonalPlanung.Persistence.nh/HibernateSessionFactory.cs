using System;
using System.Diagnostics;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PersonalPlanung.Persistence.nh.Contracts;

namespace PersonalPlanung.Persistence.nh
{
    public class HibernateSessionFactory : IHibernateSessionFactory
    {
        static readonly object InitializeSynchronisation = new object();
        ISessionFactory _sessionFactory;
        readonly IPersistenceConfiguration _persistenceConfiguration;
        readonly IHibernatePersistenceModel _persistenceModel;

        public HibernateSessionFactory(IPersistenceConfiguration persistenceConfiguration, IHibernatePersistenceModel persistenceModel)
        {
            _persistenceConfiguration = persistenceConfiguration;
            _persistenceModel = persistenceModel;
        }

        ~HibernateSessionFactory()
        {
            Dispose(false);
        }

        public IHibernateInitializationAware[] Initializers { get; set; }

        protected virtual void Dispose(bool @explicit)
        {
            if (!@explicit) return;
            if (_sessionFactory == null) return;
            _sessionFactory.Dispose();
            _sessionFactory = null;
        }

        public ISession CreateSession()
        {
            if (_sessionFactory == null)
            {
                lock (InitializeSynchronisation)
                {
                    if (_sessionFactory == null)
                        _sessionFactory = CreateSessionFactory();   
                }
            }

            var session = _sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            return session;
        }

        ISessionFactory CreateSessionFactory()
        {
            var configuration = Fluently.Configure()
                .Database(_persistenceConfiguration.GetConfiguration)
                .Mappings(_persistenceModel.AddMappings);
            var actualConfiguration = configuration.BuildConfiguration();
            CreateDatabaseWhenDebug(configuration);

            _sessionFactory = configuration.BuildSessionFactory();
            Initializers?.Each(x => x.Initialized(actualConfiguration, _sessionFactory));

            return _sessionFactory;
        }

        [Conditional("DEBUG")]
        static void CreateDatabaseWhenDebug(FluentConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true));
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}