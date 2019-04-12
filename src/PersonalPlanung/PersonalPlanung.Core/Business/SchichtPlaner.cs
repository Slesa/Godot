using System;
using System.Linq;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;

namespace PersonalPlanung.Core.Business
{
    public class SchichtPlaner
    {
        readonly ISchichtRepository _schichtRepository;
        readonly IchFindePersonal _personalFinder;
        readonly IZeitBuchungRepository _zeitBuchungen;

        public SchichtPlaner(ISchichtRepository schichtRepository, IchFindePersonal personalFinder, IZeitBuchungRepository zeitBuchungen)
        {
            _schichtRepository = schichtRepository;
            _personalFinder = personalFinder;
            _zeitBuchungen = zeitBuchungen;
        }

        public void Plane(DateTime monat)
        {
            var schichten = _schichtRepository.GetAll();
            var aktuelleSchicht = schichten.Where(x => x.Veranstaltung.BeginntAm.Month == monat.Month);
            foreach (var schicht in aktuelleSchicht)
            {
                var person = _personalFinder.FindeFür(schicht.Posten.Beginn, schicht.Posten.Ende, schicht.Posten.Rolle);
                if (person == null) continue;

                schicht.Person = person;
                var zeitbuchung = new ZeitBuchung
                {
                    Person = person,
                    Rolle = schicht.Posten.Rolle,
                    Wann = schicht.Posten.Beginn,
                    Minuten = (uint) (schicht.Posten.Ende - schicht.Posten.Beginn).TotalMinutes,
                    MinutenSatz = person.MinutenSatz
                };
                _zeitBuchungen.Add(zeitbuchung);
            }
        }
    }
}