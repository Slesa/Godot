using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;
using PersonalPlanung.Core.Model;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable PossibleNullReferenceException

namespace PersonalPlanung.Persistence.xml.Specs
{
    [Subject(typeof(PersonenXmlPersister))]
    internal class Wenn_Personen_datei_nicht_vorhanden : PersonenSpecsBase
    {
        Establish context = () => _sut = new PersonenXmlPersister();

        Because of = () => _personen = _sut.Load().ToList();

        It should_give_empty_list = () => _personen.ShouldBeEmpty();

        static List<Person> _personen;
        static PersonenXmlPersister _sut;
    }


    [Subject(typeof(PersonenXmlPersister))]
    internal class Wenn_Personen_mit_minimalen_Daten_gespeichert_werden : PersonenSpecsBase
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(PersonenXmlPersister.ListName);
            _personen = new List<Person> {new Person() {Name = "Name"}};
            _sut = new PersonenXmlPersister();
        };

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _sut.Save(_personen);

        It should_save_namen = () =>
        {
            var namen = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("Name").Value;
            namen.SequenceEqual(_personen.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Person> _personen;
        static string _filename;
        static PersonenXmlPersister _sut;
    }




    [Subject(typeof(PersonenXmlPersister))]
    internal class Wenn_Personen_gespeichert_werden : PersonenSpecsBase
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(PersonenXmlPersister.ListName);
            _personen = CreatePersonen().ToList();
            _sut = new PersonenXmlPersister();
        };

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _sut.Save(_personen);

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
        It should_save_beruf = () =>
        {
            var beruf = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("BerufName").Value;
            beruf.SequenceEqual(_personen.Select(x => x.Beruf.Name)).ShouldBeTrue();
        };
        It should_save_minutensatz = () =>
        {
            var minutensatz = from x in XDocument.Load(_filename).Root.Descendants("Person") select x.Attribute("MinutenSatz").Value;
            minutensatz.SequenceEqual(_personen.Select(x => x.MinutenSatz.ToString(CultureInfo.InvariantCulture))).ShouldBeTrue();
        };
        It should_save_einsetzbarals = () =>
        {
            var einsatzP = from x in XDocument.Load(_filename).Root.Descendants("Person").Where(x=>x.Attribute("Name")?.Value=="Picard").Descendants("EinsetzbarAls").Descendants("Rolle") select x.Attribute("Name").Value;
            einsatzP.ShouldNotBeEmpty();
            var picard = _personen[0];
            picard.EinsetzbarAls.Count.ShouldEqual(einsatzP.Count());
            foreach (var element in einsatzP)
            {
                picard.EinsetzbarAls.Select(x=>x.Name).ShouldContain(element);
            }
            var einsatzK = from x in XDocument.Load(_filename).Root.Descendants("Person").Where(x=>x.Attribute("Name")?.Value=="Kirk").Descendants("EinsetzbarAls").Descendants("Rolle") select x.Attribute("Name").Value;
            einsatzK.ShouldNotBeEmpty();
            var kirk = _personen[1];
            foreach (var element in einsatzK)
            {
                kirk.EinsetzbarAls.Select(x => x.Name).ShouldContain(element);
            }
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

        Cleanup teardown = () => File.Delete(_filename);

        Because of = () => _elements = _sut.Load();

        It should_load_namen = () =>
        {
            _elements.Select(x => x.Name).SequenceEqual(_personen.Select(x => x.Name)).ShouldBeTrue();
        };
        It should_load_vornamen = () =>
        {
            _elements.Select(x => x.Vorname).SequenceEqual(_personen.Select(x => x.Vorname)).ShouldBeTrue();
        };
        It should_load_beruf = () =>
        {
            _elements.Select(x => x.Beruf.Name).SequenceEqual(_personen.Select(x => x.Beruf.Name)).ShouldBeTrue();
        };
        It should_load_minutensatz = () =>
        {
            foreach (var element in _elements)
            {
                var person = _personen.First(x => x.Name == element.Name);
                element.MinutenSatz.ShouldEqual(person.MinutenSatz);
            }
            //_elements.Select(x => x.MinutenSatz).SequenceEqual(_personen.Select(x => x.MinutenSatz)).ShouldBeTrue();
        };

        static List<Person> _personen;
        static string _filename;
        static PersonenXmlPersister _sut;
        static IEnumerable<Person> _elements;
    }

    internal static partial class XmlFileExtension
    {
        public static XmlFile WithPerson(this XmlFile xmlFile, Person person)
        {
            xmlFile.WithLine("  <Person ")
                .WithContent("    Name='").WithContent(person.Name).WithLine("'")
                .WithContent("    Vorname='").WithContent(person.Vorname).WithLine("'")
                .WithContent("    BerufName='").WithContent(person.Beruf.Name).WithLine("'")
                .WithContent("    MinutenSatz='").WithContent(person.MinutenSatz.ToString(CultureInfo.InvariantCulture)).WithLine("'>");
            var rollen = person.EinsetzbarAls.ToList();
            if (rollen.Any())
            {
                xmlFile.WithLine("    <EinsetzbarAls>");
                foreach(var rolle in rollen)
                    xmlFile.WithContent("      <Rolle Name='").WithContent(rolle.Name).WithLine("' />");
                xmlFile.WithLine("    </EinsetzbarAls>");
            }
            xmlFile.WithLine("  </Person>");
            return xmlFile;
        }
    }

    internal class PersonenSpecsBase
    {
        protected static IEnumerable<Person> CreatePersonen()
        {
            yield return new Person
            {
                Name="Picard",
                Vorname = "Jean Luc",
                MinutenSatz = 10.0m,
                Beruf = new Beruf("Mitarbeiter"),
                EinsetzbarAls = new List<Rolle> { new Rolle("Captain"), new Rolle("Steward") }
            };
            yield return new Person
            {
                Name="Kirk",
                Vorname = "James Tiberius",
                MinutenSatz = 5.7m,
                Beruf = new Beruf("Vorgesetzter"),
                EinsetzbarAls = new List<Rolle> { new Rolle("Oldscool") }
            };
        }
    }
}