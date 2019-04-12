using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using Hibernate.Specs.Common;
using Machine.Specifications;
using NHibernate;
using NHibernate.Cfg;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using PersonalPlanung.Persistence.nh.Contracts;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming

namespace PersonalPlanung.Persistence.nh.Specs
{
    [Subject(typeof(HibernateSessionFactory))]
    internal class When_a_nhibernate_session_created_for_the_first_time
    {
        Establish context = () =>
        {
            _persistenceConfiguration = Substitute.For<IPersistenceConfiguration>();
            _persistenceConfiguration.GetConfiguration()
                .Returns(_ =>
                {
                    _getConfigurationCalled = true;
                    return new SqLiteInMemoryDatabaseConfiguration().GetConfiguration();
                });

            _persistenceModel = Substitute.For<IHibernatePersistenceModel>();
            _persistenceModel.AddMappings(Arg.Do<MappingConfiguration>(config =>
            {
                _mappingCalled = true;
                config.FluentMappings.Add<MappedClassMap>();
            }));

            _initializers = new[]
            {
                Substitute.For<IHibernateInitializationAware>(),
                Substitute.For<IHibernateInitializationAware>(),
            };
            _factory = new HibernateSessionFactory(_persistenceConfiguration, _persistenceModel)
            {
                Initializers = _initializers
            };

            _factory.CreateSession();
        };

        Because of = () => { _session = _factory.CreateSession(); };

        It should_retrieve_the_persistence_configuration =
            () => _getConfigurationCalled.ShouldBeTrue();

        It should_add_mappings_from_the_persistence_model =
            () => _mappingCalled.ShouldBeTrue();

        It should_invoke_the_initializers_before_initialization =
            () => _initializers.Each(x => x.Received().Initialized(Arg.Any<Configuration>(), Arg.Any<ISessionFactory>()));

        It should_be_able_to_create_a_session =
            () => _session.ShouldNotBeNull();

        It should_create_a_session_that_flushes_on_commit =
            () => _session.FlushMode.ShouldEqual(FlushMode.Commit);

        static HibernateSessionFactory _factory;
        static ISession _session;
        static IHibernateInitializationAware[] _initializers;
        static IPersistenceConfiguration _persistenceConfiguration;
        static IHibernatePersistenceModel _persistenceModel;
        static bool _getConfigurationCalled;
        static bool _mappingCalled;
    }


    [Subject(typeof(HibernateSessionFactory))]
    internal class When_a_nhibernate_session_created
    {
        Establish context = () =>
        {
            _persistenceConfiguration = Substitute.For<IPersistenceConfiguration>();
            _persistenceConfiguration.GetConfiguration()
                .Returns(new SqLiteInMemoryDatabaseConfiguration().GetConfiguration());
            _persistenceModel = Substitute.For<IHibernatePersistenceModel>();
            _persistenceModel.AddMappings(Arg.Do<MappingConfiguration>(config => config.FluentMappings.Add<MappedClassMap>()));

            _initializers = new[]
            {
                Substitute.For<IHibernateInitializationAware>()
            };

            _factory = new HibernateSessionFactory(_persistenceConfiguration, _persistenceModel)
            {
                Initializers = _initializers
            };

            _factory.CreateSession();
        };

        Because of = () => { _session = _factory.CreateSession(); };

        It should_be_able_to_create_a_session =
            () => _session.ShouldNotBeNull();

        It should_create_a_session_that_flushes_on_commit =
            () => _session.FlushMode.ShouldEqual(FlushMode.Commit);

        static IPersistenceConfiguration _persistenceConfiguration;
        static IHibernatePersistenceModel _persistenceModel;
        static IHibernateInitializationAware[] _initializers;
        static HibernateSessionFactory _factory;
        static ISession _session;
    }


    public class MappedClass
    {
        protected MappedClass() {}
        public virtual int Id { get; set; }
    }

    public class MappedClassMap : ClassMap<MappedClass>
    {
        public MappedClassMap()
        {
            Id(x => x.Id);
        }
    }
}