using FluentNHibernate.Cfg;

namespace PersonalPlanung.Persistence.nh.Contracts
{
    public interface IMappingContributor
    {
        void Apply(MappingConfiguration configuration);
    }
}