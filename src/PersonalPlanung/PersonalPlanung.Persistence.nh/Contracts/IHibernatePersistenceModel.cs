using FluentNHibernate.Cfg;

namespace PersonalPlanung.Persistence.nh.Contracts
{
    public interface IHibernatePersistenceModel
    {
        void AddMappings(MappingConfiguration configuration);
    }
}