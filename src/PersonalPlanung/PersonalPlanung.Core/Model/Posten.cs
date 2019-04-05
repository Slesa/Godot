using System;
using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{Beginn} - {Ende}: {Rolle} ({Standort})")]
    public class Posten
    {
        public Posten(DateTime startZeit, DateTime endeZeit, Rolle rolle, Standort standort)
        {
            Beginn = startZeit;
            Ende = endeZeit;
            Rolle = rolle;
            Standort = standort;
        }

        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }
        public Rolle Rolle { get; set; }
        public Standort Standort { get; set; }
    }
}