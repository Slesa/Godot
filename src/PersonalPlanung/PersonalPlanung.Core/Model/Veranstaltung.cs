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

        List<Aufgabe> _aufgaben;
        public List<Aufgabe> Aufgaben
        {
            get => _aufgaben ?? (_aufgaben = new List<Aufgabe>());
            set => _aufgaben = value;
        }
    }
}