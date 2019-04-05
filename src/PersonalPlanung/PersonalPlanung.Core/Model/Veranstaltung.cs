using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{Name} {BeginntAm} - {EndetAm}")]
    public class Veranstaltung
    {
        public Veranstaltung(string name, DateTime beginntAm, DateTime endetAm)
        {
            Name = name;
            BeginntAm = beginntAm;
            EndetAm = endetAm;
        }

        public string Name { get; set; }
        public DateTime BeginntAm { get; set; }
        public DateTime EndetAm { get; set; }

        public List<Posten> Posten { get; } = new List<Posten>();
    }
}