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
    [Subject(typeof(BerufeXmlPersister))]
    internal class Wenn_Berufe_datei_nicht_vorhanden : BerufeSpecsBase
    {
        Establish context = () => _sut = new BerufeXmlPersister();

        Because of = () => _berufe = _sut.Load().ToList();

        It should_give_empty_list = () => _berufe.ShouldBeEmpty();

        static List<Beruf> _berufe;
        static BerufeXmlPersister _sut;
    }


    [Subject(typeof(BerufeXmlPersister))]
    internal class Wenn_Berufe_gespeichert_werden : BerufeSpecsBase
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(BerufeXmlPersister.ListName);
            _berufe = CreateBerufe().ToList();
            _sut = new BerufeXmlPersister();
        };

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _sut.Save(_berufe);

        It should_save_namen = () =>
        {
            var namen = from x in XDocument.Load(_filename).Root.Descendants("Beruf") select x.Attribute("Name").Value;
            namen.SequenceEqual(_berufe.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Beruf> _berufe;
        static string _filename;
        static BerufeXmlPersister _sut;
    }


    [Subject(typeof(BerufeXmlPersister))]
    internal class Wenn_Berufe_geladen_werden : BerufeSpecsBase
    {
        Establish context = () =>
        {
            _berufe = CreateBerufe().ToList();
            _filename = FileNamer.GetFilenameFor(BerufeXmlPersister.ListName);
            new XmlFile().WithLine("<?xml version='1.0' encoding='utf-8'?>")
                .WithLine("<PersonalPlanung xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>")
                .WithLine("<Berufe>")
                .WithContent("<Beruf Name='").WithContent(_berufe[0].Name).WithLine("' />")
                .WithContent("<Beruf Name='").WithContent(_berufe[1].Name).WithLine("' />")
                .WithLine("</Berufe> ")
                .WithLine("</PersonalPlanung>")
                .AsFile(_filename);
            _sut = new BerufeXmlPersister();
        };

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _elements = _sut.Load();

        It should_load_namen = () =>
        {
            _elements.Select(x => x.Name).SequenceEqual(_berufe.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Beruf> _berufe;
        static string _filename;
        static BerufeXmlPersister _sut;
        static IEnumerable<Beruf> _elements;
    }

    internal class BerufeSpecsBase
    {
        protected static IEnumerable<Beruf> CreateBerufe()
        {
            yield return new Beruf("Number One");
            yield return new Beruf("Watchman");
        }
    }
}