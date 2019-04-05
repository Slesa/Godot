using System.Collections.Generic;
using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{Name} ({Vorname})")]
    public class Person : ValueObject<Person>
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        [IgnoreValue]
        public Status Status { get; set; }
        [IgnoreValue]
        public List<Rolle> EinsetzbarAls { get; set; } = new List<Rolle>();
    }
}