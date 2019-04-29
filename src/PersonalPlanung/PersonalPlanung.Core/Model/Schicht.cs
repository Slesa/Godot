using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{Aufgabe.Rolle.Name}: {Person.Name}, {Veranstaltung.Name}")]
    public class Schicht : ValueObject<Schicht>
    {
        public Aufgabe Aufgabe { get; set; }
        public Person Person { get; set; }
        public Veranstaltung Veranstaltung { get; set; }
    }
}