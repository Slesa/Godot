using System;
using System.Collections.Generic;
using Machine.Specifications;
using NSubstitute;
using PersonalPlanung.Core.Business;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace PersonalPlanung.Core.Specs.Business
{
    [Subject(typeof(PersonalFinder))]
    internal class Wenn_nach_einer_person_ohne_personal_gesucht_wird : PersonalFinderSpecs
    {
        Establish context = () => _finder = new PersonalFinder(Personen, ZeitBuchungen);
        Because of = () => _person = _finder.FindeFür(DateTime.Now, DateTime.Now, OrdnerRolle);
        It sollte_nichts_finden = () => { _person.ShouldBeNull(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());

        static PersonalFinder _finder;
        static Person _person;
    }


    [Subject(typeof(PersonalFinder))]
    internal class Wenn_nach_einer_person_ohne_gesuchte_rolle_gesucht_wird : PersonalFinderSpecs
    {
        Establish context = () =>
        {
            var personen = new List<Person> { Hausmeister };
            Personen.GetAll().Returns(personen);

            _finder = new PersonalFinder(Personen, ZeitBuchungen);
        };
        Because of = () => _person = _finder.FindeFür(DateTime.Now, DateTime.Now, OrdnerRolle);
        It sollte_nichts_finden = () => { _person.ShouldBeNull(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());

        static PersonalFinder _finder;
        static Person _person;
    }


    [Subject(typeof(PersonalFinder))]
    internal class Wenn_nach_einer_person_mit_passender_rolle_gesucht_wird : PersonalFinderSpecs
    {
        Establish context = () =>
        {
            var personen = new List<Person> { Hausmeister };
            Personen.GetAll().Returns(personen);

            _finder = new PersonalFinder(Personen, ZeitBuchungen);
        };
        Because of = () => _person = _finder.FindeFür(DateTime.Now, DateTime.Now, HausmeisterRolle);
        It sollte_hausmeister_finden = () => { _person.ShouldEqual(Hausmeister); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());

        static PersonalFinder _finder;
        static Person _person;
    }


    [Subject(typeof(PersonalFinder))]
    internal class Wenn_gesuchte_person_bereits_zu_viele_stunden_an_dem_tag_hat : PersonalFinderSpecs
    {
        Establish context = () =>
        {
            var buchungen = new List<ZeitBuchung> { new ZeitBuchung {Person = Hausmeister, Minuten = 121, Wann = DateTime.Now } };
            ZeitBuchungen.GetAll().Returns(buchungen);

            var personen = new List<Person> { Hausmeister };
            Personen.GetAll().Returns(personen);

            _finder = new PersonalFinder(Personen, ZeitBuchungen);
        };
        Because of = () => _person = _finder.FindeFür(DateTime.Now, DateTime.Now.AddMinutes(8*60), HausmeisterRolle);
        It sollte_nichts_finden = () => { _person.ShouldBeNull(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());

        static PersonalFinder _finder;
        static Person _person;
    }


    [Subject(typeof(PersonalFinder))]
    internal class Wenn_gesuchte_person_bereits_vor_11h_gearbeitet_hat : PersonalFinderSpecs
    {
        Establish context = () =>
        {
            var buchungen = new List<ZeitBuchung> { new ZeitBuchung {Person = Hausmeister, Minuten = 121, Wann = DateTime.Now.AddMinutes(-11*60+1) } };
            ZeitBuchungen.GetAll().Returns(buchungen);

            var personen = new List<Person> { Hausmeister };
            Personen.GetAll().Returns(personen);

            _finder = new PersonalFinder(Personen, ZeitBuchungen);
        };
        Because of = () => _person = _finder.FindeFür(DateTime.Now, DateTime.Now.AddMinutes(8*60), HausmeisterRolle);
        It sollte_nichts_finden = () => { _person.ShouldBeNull(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());

        static PersonalFinder _finder;
        static Person _person;
    }


    [Subject(typeof(PersonalFinder))]
    internal class Wenn_student_dadurch_mehr_als_450_verdient : PersonalFinderSpecs
    {
        Establish context = () =>
        {
            var buchungen = new List<ZeitBuchung> { new ZeitBuchung {Person = Student, Minuten = 28*60+1, MinutenSatz = 12.5M/60M, Wann = DateTime.Now.AddMinutes(-12*60) } };
            ZeitBuchungen.GetAll().Returns(buchungen);

            var personen = new List<Person> { Student };
            Personen.GetAll().Returns(personen);

            _finder = new PersonalFinder(Personen, ZeitBuchungen);
        };
        Because of = () => _person = _finder.FindeFür(DateTime.Now, DateTime.Now.AddMinutes(8*60), HausmeisterRolle);
        It sollte_nichts_finden = () => { _person.ShouldBeNull(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());

        static PersonalFinder _finder;
        static Person _person;
    }



    internal class PersonalFinderSpecs
    {
        static PersonalFinderSpecs()
        {
            Personen = Substitute.For<IPersonRepository>();
            ZeitBuchungen = Substitute.For<IZeitBuchungRepository>();
        }

        protected static Rolle OrdnerRolle => new Rolle("Ordner");
        protected static Rolle HausmeisterRolle => new Rolle("Hausmeister");
        protected static Person Hausmeister =>
            new Person
            {
                Name = "Manni",
                Beruf = Beruf.Kollege,
                EinsetzbarAls = new List<Rolle> {HausmeisterRolle}
            };
        protected static Person Student =>
            new Person
            {
                Name = "Harald",
                Beruf = Beruf.Student,
                MinutenSatz = 12.5M/60M,
                EinsetzbarAls = new List<Rolle> {HausmeisterRolle}
            };

        protected static IPersonRepository Personen;
        protected static IZeitBuchungRepository ZeitBuchungen;
    }

}