using System;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Gui.ViewModels
{
    public class ZeitBuchungsViewModel
    {
        public ZeitBuchungsViewModel(ZeitBuchung x)
        {
            Wann = x.Wann;
            Zeit = GetZeit(x.Minuten);
            MinutenSatz = x.MinutenSatz==0m ? "" : x.MinutenSatz.ToString();
            RollenName = x.Rolle.Name;
            PersonenName = x.Person.Name;
            if (!string.IsNullOrWhiteSpace(x.Person.Vorname)) PersonenName += ", " + x.Person.Vorname;
            Verbucht = x.Verbucht;
        }

        public DateTime Wann { get; }
        public string Zeit { get; }
        public string MinutenSatz { get; }
        public string RollenName { get; }
        public string PersonenName { get; }
        public bool Verbucht { get; }

        string GetZeit(uint xMinuten)
        {
            var hours = xMinuten / 60;
            var mins = xMinuten % 60;
            return $"{hours}h{mins:00}";
        }
    }
}