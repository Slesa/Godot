using System;

namespace PersonalPlanung.Core.Model
{
    public class ZeitBuchung : ValueObject<ZeitBuchung>
    {
        public DateTime Wann { get; set; }
        public uint Minuten { get; set; }
        public decimal MinutenSatz { get; set; }
        public Rolle Rolle { get; set; }
        [IgnoreValue]
        public Person Person { get; set; }
        [IgnoreValue]
        public bool Verbucht { get; set; }
    }
}