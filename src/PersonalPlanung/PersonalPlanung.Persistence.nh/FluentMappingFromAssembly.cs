using System.Reflection;
using FluentNHibernate.Cfg;

namespace PersonalPlanung.Persistence.nh
{
    public class FluentMappingFromAssembly
    {
        Assembly _assembly;

        public FluentMappingFromAssembly WithAssembly(string assemblyName)
        {
            _assembly = Assembly.LoadFrom(assemblyName);
            return this;
        }

        public void ApplyMappings(MappingConfiguration configureation)
        {
            if (_assembly != null) configureation.FluentMappings.AddFromAssembly(_assembly);
        }
    }
}