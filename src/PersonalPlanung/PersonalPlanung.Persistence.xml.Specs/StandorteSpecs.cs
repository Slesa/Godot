// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml.Specs
{
    [Subject(typeof(StandorteXmlPersister))]
    internal class Wenn_Standorte_gespeichert_werden : StandorteSpecsBase
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(StandorteXmlPersister.ListName);
            _standorte = CreateStandorte().ToList();
            _sut = new StandorteXmlPersister();
        };

        Cleanup teardown = () =>
        {
            File.Delete(_filename);
        };

        Because of = () =>
        {
            _sut.Save(_standorte);
        };

        It should_save_namen = () =>
        {
            var namen = from x in XDocument.Load(_filename).Root.Descendants("Standort") select x.Attribute("Name").Value;
            namen.SequenceEqual(_standorte.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Standort> _standorte;
        static string _filename;
        static StandorteXmlPersister _sut;
    }


    [Subject(typeof(StandorteXmlPersister))]
    internal class Wenn_Standorte_geladen_werden : StandorteSpecsBase
    {
        Establish context = () =>
        {
            _standorte = CreateStandorte().ToList();
            _filename = FileNamer.GetFilenameFor(StandorteXmlPersister.ListName);
            new XmlFile().WithLine("<?xml version='1.0' encoding='utf-8'?>")
                .WithLine("<PersonalPlanung xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>")
                .WithLine("<Standorte>")
                .WithContent("<Standort Name='").WithContent(_standorte[0].Name).WithLine("' />")
                .WithContent("<Standort Name='").WithContent(_standorte[1].Name).WithLine("' />")
                .WithLine("</Standorte> ")
                .WithLine("</PersonalPlanung>")
                .AsFile(_filename);
            _sut = new StandorteXmlPersister();
        };

        Cleanup teardown = () =>
        {
            File.Delete(_filename);
        };

        Because of = () =>
        {
            _elements = _sut.Load();
        };

        It should_load_namen = () =>
        {
            _elements.Select(x => x.Name).SequenceEqual(_standorte.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Standort> _standorte;
        static string _filename;
        static StandorteXmlPersister _sut;
        static IEnumerable<Standort> _elements;
    }

    internal class StandorteSpecsBase
    {
        protected static IEnumerable<Standort> CreateStandorte()
        {
            yield return new Standort("Schranke");
            yield return new Standort("Haupteingang");
        }
    }
}