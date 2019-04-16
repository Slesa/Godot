using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;
using PersonalPlanung.Core.Model;
// ReSharper disable InconsistentNaming

namespace PersonalPlanung.Persistence.xml.Specs
{
    [Subject(typeof(PersonenXmlPersister))]
    internal class Wenn_Personen_gespeichert_werden : PersonenSpecsBase
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(PersonenXmlPersister.ListName);
            _personen = CreatePersonen().ToList();
            _sut = new PersonenXmlPersister();
        };

        Cleanup teardown = () =>
        {
             File.Delete(_filename);
        };

        Because of = () =>
        {
            _sut.Save(_personen);
        };

        It should_save_namen = () =>
        {
            var namen = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("Name").Value;
            namen.SequenceEqual(_personen.Select(x => x.Name)).ShouldBeTrue();
        };
        It should_save_vornamen = () =>
        {
            var vornamen = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("Vorname").Value;
            vornamen.SequenceEqual(_personen.Select(x => x.Vorname)).ShouldBeTrue();
        };
        It should_save_status = () =>
        {
            var status = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("StatusName").Value;
            status.SequenceEqual(_personen.Select(x => x.Status.Name)).ShouldBeTrue();
        };
        It should_save_minutensatz = () =>
        {
            var minutensatz = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("MinutenSatz").Value;
            minutensatz.SequenceEqual(_personen.Select(x => x.MinutenSatz.ToString())).ShouldBeTrue();
        };

        static List<Person> _personen;
        static string _filename;
        static PersonenXmlPersister _sut;
    }


    [Subject(typeof(PersonenXmlPersister))]
    internal class Wenn_Personen_geladen_werden : PersonenSpecsBase
    {
        Establish context = () =>
        {
            _personen = CreatePersonen().ToList();
            _filename = FileNamer.GetFilenameFor(PersonenXmlPersister.ListName);
            new XmlFile().WithLine("<?xml version='1.0' encoding='utf-8'?>")
                .WithLine("<PersonalPlanung xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>")
                .WithLine("<Personen>")
                .WithPerson(_personen[0])
                .WithPerson(_personen[1])
                .WithLine("</Personen> ")
                .WithLine("</PersonalPlanung>")
                .AsFile(_filename);
            _sut = new PersonenXmlPersister();
        };

        Cleanup teardown = () =>
        {
//            File.Delete(_filename);
        };

        Because of = () =>
        {
            _elements = _sut.Load();
        };

        It should_load_namen = () =>
        {
            _elements.Select(x => x.Name).SequenceEqual(_personen.Select(x => x.Name)).ShouldBeTrue();
        };
        It should_load_vornamen = () =>
        {
            _elements.Select(x => x.Vorname).SequenceEqual(_personen.Select(x => x.Vorname)).ShouldBeTrue();
        };
        It should_load_status = () =>
        {
            _elements.Select(x => x.Status.Name).SequenceEqual(_personen.Select(x => x.Status.Name)).ShouldBeTrue();
        };
        It should_load_minutensatz = () =>
        {
            _elements.Select(x => x.MinutenSatz).SequenceEqual(_personen.Select(x => x.MinutenSatz)).ShouldBeTrue();
        };

        static List<Person> _personen;
        static string _filename;
        static PersonenXmlPersister _sut;
        static IEnumerable<Person> _elements;
    }

    internal static class XmlFileExtension
    {
        public static XmlFile WithPerson(this XmlFile xmlFile, Person person)
        {
            xmlFile.WithLine("  <Person ")
                .WithContent("    Name='").WithContent(person.Name).WithLine("'")
                .WithContent("    Vorname='").WithContent(person.Vorname).WithLine("'")
                .WithContent("    StatusName='").WithContent(person.Status.Name).WithLine("'")
                .WithContent("    Minutensatz='").WithContent(person.MinutenSatz.ToString(CultureInfo.CurrentCulture)).WithLine("'")
                .WithLine("    />");
            return xmlFile;
        }
    }

    internal class PersonenSpecsBase
    {
        protected static IEnumerable<Person> CreatePersonen()
        {
            yield return new Person { Name="Picard", Vorname = "Jean Luc", MinutenSatz = 10.0m, Status = new Core.Model.Status("Mitarbeiter")};
            yield return new Person { Name="Kirk", Vorname = "James Tiberius", MinutenSatz = 5.7m, Status = new Core.Model.Status("Vorgesetzter")};
        }
    }
}