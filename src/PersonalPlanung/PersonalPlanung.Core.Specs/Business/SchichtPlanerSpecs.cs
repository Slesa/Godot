using System;
using System.Collections.Generic;
using System.Linq;
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
    [Subject(typeof(SchichtPlaner))]
    internal class Wenn_eine_leere_schicht_geplant_wird : SchichtPlanerSpecs
    {
        Establish context = () => _planer = new SchichtPlaner(Schichten, PersonenFinder, ZeitBuchungen);
        Because of = () => _planer.Plane(DateTime.Now);
        It sollte_nichts_planen = () => { ; };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());
        static SchichtPlaner _planer;
    }


    [Subject(typeof(SchichtPlaner))]
    internal class Wenn_eine_schicht_ohne_personen_geplant_wird : SchichtPlanerSpecs
    {
        Establish context = () =>
        {
            _schichten = OrdnerSchicht();
            Schichten.GetAll().Returns(_schichten);
            _planer = new SchichtPlaner(Schichten, PersonenFinder, ZeitBuchungen);
        };
        Because of = () => _planer.Plane(_schichten.First().Veranstaltung.BeginntAm);
        It sollte_nichts_planen = () => { _schichten.Where(x => x.Person!=null).ShouldBeEmpty(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());
        static SchichtPlaner _planer;
        static List<Schicht> _schichten;
    }

    [Subject(typeof(SchichtPlaner))]
    internal class Wenn_eine_schicht_ohne_person_mit_dieser_rolle_geplant_wird : SchichtPlanerSpecs
    {
        Establish context = () =>
        {
            Schichten.GetAll().Returns(_schichten);
            _schichten = OrdnerSchicht();
            Schichten.GetAll().Returns(_schichten);
            _planer = new SchichtPlaner(Schichten, PersonenFinder, ZeitBuchungen);
        };
        Because of = () => _planer.Plane(_schichten.First().Veranstaltung.BeginntAm);
        It sollte_nichts_planen = () => { _schichten.Where(x => x.Person!=null).ShouldBeEmpty(); };
        It sollte_keine_buchung_erzeugen = () => ZeitBuchungen.DidNotReceive().Add(Arg.Any<ZeitBuchung>());
        static SchichtPlaner _planer;
        static List<Schicht> _schichten;
    }


    [Subject(typeof(SchichtPlaner))]
    internal class Wenn_eine_minimale_schicht_geplant_wird : SchichtPlanerSpecs
    {
        Establish context = () =>
        {
            Schichten.GetAll().Returns(_schichten);
            _schichten = OrdnerSchicht();
            PersonenFinder.FindeFür(Arg.Any<DateTime>(), Arg.Any<DateTime>(), OrdnerRolle).Returns(Ordner);
            Schichten.GetAll().Returns(_schichten);
            _planer = new SchichtPlaner(Schichten, PersonenFinder, ZeitBuchungen);
        };
        Because of = () => _planer.Plane(_schichten.First().Veranstaltung.BeginntAm);
        It sollte_ordner_einplanen = () => { _schichten.Count(x => x.Person != null).ShouldEqual(1); };
        It sollte_buchung_eintragen = () => ZeitBuchungen.Received().Add(Arg.Any<ZeitBuchung>());
        static SchichtPlaner _planer;
        static List<Schicht> _schichten;
    }


    internal class SchichtPlanerSpecs
    {
        static SchichtPlanerSpecs()
        {
            Schichten = Substitute.For<ISchichtRepository>();
            PersonenFinder = Substitute.For<IchFindePersonal>();
            PersonenFinder.FindeFür(Arg.Any<DateTime>(), Arg.Any<DateTime>(), HausmeisterRolle).Returns(Hausmeister);
            ZeitBuchungen = Substitute.For<IZeitBuchungRepository>();
        }


        protected static Rolle OrdnerRolle => new Rolle("Ordner");
        protected static Rolle HausmeisterRolle => new Rolle("Hausmeister");
        protected static Person Hausmeister =>
            new Person
            {
                Name = "Manni",
                Beruf = Beruf.Kollege,
                EinsetzbarAls = new List<Rolle> { HausmeisterRolle }
            };
        protected static Person Ordner =>
            new Person
            {
                Name = "Karl",
                Beruf = Beruf.Kollege,
                EinsetzbarAls = new List<Rolle> { OrdnerRolle }
            };
        protected static Person Student =>
            new Person
            {
                Name = "Harald",
                Beruf = Beruf.Student,
                MinutenSatz = 12.5M / 60M,
                EinsetzbarAls = new List<Rolle> { HausmeisterRolle }
            };

        protected static List<Schicht> OrdnerSchicht()
        {
            var datum = new DateTime(2019, 12, 12);
            var veranstaltung = new Veranstaltung("Messe", datum, datum);
            var aufgabe = new Aufgabe(
                new DateTime(datum.Year, datum.Month, datum.Day, 8, 30, 0),
                new DateTime(datum.Year, datum.Month, datum.Day, 12, 30, 0),
                OrdnerRolle,
                new Standort("Haupteingang"));
            return new List<Schicht> { new Schicht { Aufgabe = aufgabe, Veranstaltung = veranstaltung } };
        }
        protected static ISchichtRepository Schichten;
        protected static IchFindePersonal PersonenFinder;
        protected static IZeitBuchungRepository ZeitBuchungen;
    }
}
