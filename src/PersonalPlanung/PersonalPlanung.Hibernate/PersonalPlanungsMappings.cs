using FluentNHibernate.Cfg;
using PersonalPlanung.Persistence.nh;
using PersonalPlanung.Persistence.nh.Contracts;

namespace PersonalPlanung.Hibernate
{
    public class PersonalPlanungsMappings : IMappingContributor
    {
        public void Apply(MappingConfiguration configuration)
        {
            new FluentMappingFromAssembly().WithAssembly(typeof(PersonalPlanungsMappings).Assembly.CodeBase).ApplyMappings(configuration);
        }
    }
}