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
            _zeitBuchungen.BeginChanges();
            foreach (var schicht in aktuelleSchicht)
            {
                var person = _personalFinder.FindeFür(schicht.Aufgabe.Beginn, schicht.Aufgabe.Ende, schicht.Aufgabe.Rolle);
                if (person == null) continue;

                schicht.Person = person;
                var zeitbuchung = new ZeitBuchung
                {
                    Person = person,
                    Rolle = schicht.Aufgabe.Rolle,
                    Wann = schicht.Aufgabe.Beginn,
                    Minuten = (uint) (schicht.Aufgabe.Ende - schicht.Aufgabe.Beginn).TotalMinutes,
                    MinutenSatz = person.MinutenSatz
                };
                _zeitBuchungen.Add(zeitbuchung);
            }
            _schichtRepository.Save();
            _zeitBuchungen.Save();
        }
    }
}