using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Rolle : ValueObject<Rolle>
    {
        public Rolle(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}