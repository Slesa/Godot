using System;

namespace PersonalPlanung.Core.Model
{
    public class ZeitBuchung
    {
        public DateTime Wann { get; set; }
        public uint Minuten { get; set; }
        public decimal MinutenSatz { get; set; }
        public bool Verbucht { get; set; }
        public Person Person { get; set; }
        public Rolle Rolle { get; set; }
    }
}