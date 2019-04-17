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
        public decimal MinutenSatz { get; set; }
        [IgnoreValue]
        public Beruf Beruf { get; set; }
        [IgnoreValue]
        public List<Rolle> EinsetzbarAls { get; set; } = new List<Rolle>();
    }
}