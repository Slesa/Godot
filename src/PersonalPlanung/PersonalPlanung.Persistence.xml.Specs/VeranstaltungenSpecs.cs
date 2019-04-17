using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;
using PersonalPlanung.Core.Model;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

namespace PersonalPlanung.Persistence.xml.Specs
{
    [Subject(typeof(VeranstaltungenXmlPersister))]
    internal class Wenn_Veranstaltungen_datei_nicht_vorhanden : VeranstaltungenSpecsBase
    {
        Establish context = () => _sut = new VeranstaltungenXmlPersister();

        Because of = () => _veranstaltungen = _sut.Load().ToList();

        It should_give_empty_list = () => _veranstaltungen.ShouldBeEmpty();

        static List<Veranstaltung> _veranstaltungen;
        static VeranstaltungenXmlPersister _sut;
    }


    [Subject(typeof(VeranstaltungenXmlPersister))]
    internal class Wenn_Veranstaltungen_gespeichert_werden : VeranstaltungenSpecsBase
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(VeranstaltungenXmlPersister.ListName);
            _veranstaltungen = CreateVeranstaltungen().ToList();
            _sut = new VeranstaltungenXmlPersister();
        };

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _sut.Save(_veranstaltungen);

        It should_save_namen = () =>
        {
            var namen = from x in XDocument.Load(_filename).Root.Descendants("Veranstaltung") select x.Attribute("Name").Value;
            namen.SequenceEqual(_veranstaltungen.Select(x => x.Name)).ShouldBeTrue();
        };
        It should_save_beginntam = () =>
        {
            var beginn = from x in XDocument.Load(_filename).Root.Descendants("Veranstaltung") select x.Attribute("BeginntAm").Value;
            beginn.SequenceEqual(_veranstaltungen.Select(x => x.BeginntAm.ToString(VeranstaltungsDto.DateFormat))).ShouldBeTrue();
        };
        It should_save_endetam = () =>
        {
            var endet = from x in XDocument.Load(_filename).Root.Descendants("Veranstaltung") select x.Attribute("EndetAm").Value;
            endet.SequenceEqual(_veranstaltungen.Select(x => x.EndetAm.ToString(VeranstaltungsDto.DateFormat))).ShouldBeTrue();
        };

        static List<Veranstaltung> _veranstaltungen;
        static string _filename;
        static VeranstaltungenXmlPersister _sut;
    }


    [Subject(typeof(VeranstaltungenXmlPersister))]
    internal class Wenn_Veranstaltungen_geladen_werden : VeranstaltungenSpecsBase
    {
        Establish context = () =>
        {
            _veranstaltungen = CreateVeranstaltungen().ToList();
            _filename = FileNamer.GetFilenameFor(VeranstaltungenXmlPersister.ListName);
            new XmlFile().WithLine("<?xml version='1.0' encoding='utf-8'?>")
                .WithLine("<PersonalPlanung xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>")
                .WithLine("<Veranstaltungen>")
                .WithVeranstaltungen(_veranstaltungen[0])
                .WithVeranstaltungen(_veranstaltungen[1])
                .WithLine("</Veranstaltungen> ")
                .WithLine("</PersonalPlanung>")
                .AsFile(_filename);
            _sut = new VeranstaltungenXmlPersister();
        };

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _elements = _sut.Load();

        It should_load_namen = () =>
        {
            _elements.Select(x => x.Name).SequenceEqual(_veranstaltungen.Select(x => x.Name)).ShouldBeTrue();
        };
        It should_load_beginntam = () =>
        {
            _elements.Select(x => x.BeginntAm).SequenceEqual(_veranstaltungen.Select(x => x.BeginntAm)).ShouldBeTrue();
        };
        It should_load_endetam = () =>
        {
            _elements.Select(x => x.EndetAm).SequenceEqual(_veranstaltungen.Select(x => x.EndetAm)).ShouldBeTrue();
        };

        static List<Veranstaltung> _veranstaltungen;
        static string _filename;
        static VeranstaltungenXmlPersister _sut;
        static IEnumerable<Veranstaltung> _elements;
    }


    internal static partial class XmlFileExtension
    {
        public static XmlFile WithVeranstaltungen(this XmlFile xmlFile, Veranstaltung veranstaltung)
        {
            xmlFile.WithLine("  <Veranstaltung ")
                .WithContent("    Name='").WithContent(veranstaltung.Name).WithLine("'")
                .WithContent("    BeginntAm='").WithContent(veranstaltung.BeginntAm.ToString(VeranstaltungsDto.DateFormat)).WithLine("'")
                .WithContent("    EndetAm='").WithContent(veranstaltung.EndetAm.ToString(VeranstaltungsDto.DateFormat)).WithLine("'>")
                .WithLine("      <Aufgaben>");
            foreach (var aufgabe in veranstaltung.Aufgaben)
                xmlFile.WithAufgabe(aufgabe);
            xmlFile.WithLine("      </Aufgaben>")
                .WithLine("    </Veranstaltung>");
            return xmlFile;
        }
        public static XmlFile WithAufgabe(this XmlFile xmlFile, Aufgabe aufgabe)
        {
            return xmlFile;
        }
    }

    internal class VeranstaltungenSpecsBase
    {
        protected static IEnumerable<Veranstaltung> CreateVeranstaltungen()
        {
            var sektempfang = new Veranstaltung("Sektempfang", new DateTime(2017, 7, 16, 10, 0, 0), new DateTime(2017, 7, 16, 12, 0, 0) );
            sektempfang.Aufgaben = new List<Aufgabe>
            {
                new Aufgabe(new DateTime(2017, 7, 16, 10, 0, 0), new DateTime(2017, 7, 16, 11, 0, 0), new Rolle("Einlass"), new Standort("Haupteingang")),
                new Aufgabe(new DateTime(2017, 7, 16, 10, 0, 0), new DateTime(2017, 7, 16, 12, 0, 0), new Rolle("Ordner"), new Standort("Saal")),
                new Aufgabe(new DateTime(2017, 7, 16, 11, 0, 0), new DateTime(2017, 7, 16, 12, 0, 0), new Rolle("Serviette"), new Standort("Saal")),
            };
            yield return sektempfang;
            var berlinale = new Veranstaltung("Berlinale", new DateTime(2017, 8, 20, 8, 0, 0), new DateTime(2017, 8, 27, 23, 0, 0));
            yield return berlinale;
        }
    }
}