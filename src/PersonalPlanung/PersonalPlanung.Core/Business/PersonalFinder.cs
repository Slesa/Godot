using System;
using System.Linq;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;

namespace PersonalPlanung.Core.Business
{
    public class PersonalFinder : IchFindePersonal
    {
        readonly IPersonRepository _personRepository;
        readonly IZeitBuchungRepository _zeitBuchungen;

        public PersonalFinder(IPersonRepository personRepository, IZeitBuchungRepository zeitBuchungen)
        {
            _personRepository = personRepository;
            _zeitBuchungen = zeitBuchungen;
        }

        public Person FindeFür(DateTime von, DateTime bis, Rolle rolle)
        {
            var minuten = (bis - von).TotalMinutes;
            var personal = _personRepository.GetAll().Where(x => x.EinsetzbarAls.Contains(rolle));
            foreach (var person in personal)
            {
                var gearbeitet = _zeitBuchungen.GetAll()
                    .Where(x => x.Person == person)
                    .Where(x => x.Wann.Day == von.Day && x.Wann.Month == von.Month && x.Wann.Year == von.Year)
                    .Sum(x => x.Minuten);
                if (gearbeitet + minuten > 600) continue; // 10h max am Tag

                var letzteSchicht = _zeitBuchungen.GetAll()
                    .Where(x => x.Person == person)
                    .Select(x => x.Wann)
                    .DefaultIfEmpty(DateTime.MinValue)
                    .Max();
                if (letzteSchicht != DateTime.MinValue)
                {
                    var freizeit = (von - letzteSchicht).TotalMinutes;
                    if (freizeit < 11 * 60) continue; // 11h zwischen Schichten
                }

                if (person.Beruf == Beruf.Student)
                {
                    var verdient = _zeitBuchungen.GetAll()
                        .Where(x => x.Person == person)
                        .Where(x => x.Wann.Month == von.Month && x.Wann.Year == von.Year)
                        .Sum(x => x.Minuten*x.MinutenSatz);
                    if (verdient + (decimal)minuten * person.MinutenSatz>450M) continue;
                }
                return person;
            }
            return null;
        }

    }
}