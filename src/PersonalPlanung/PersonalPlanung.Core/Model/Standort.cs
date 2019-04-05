using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{Name}")]
    public class Standort : ValueObject<Standort>
    {
        public Standort(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}