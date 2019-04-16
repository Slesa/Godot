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
    internal class Wenn_Rollen_gespeichert_werden
    {
        Establish context = () =>
        {
            _filename = FileNamer.GetFilenameFor(RollenXmlPersister.ListName);
            _rollen = new List<Rolle>
            {
                new Rolle("Captain"),
                new Rolle("Lieutnant"),
            };
            _sut = new RollenXmlPersister();
        };

        Cleanup teardown = () =>
        {
            File.Delete(_filename);
        };

        Because of = () =>
        {
            _sut.Save(_rollen);
        };

        It should_save_namen = () =>
        {
            var namen = from x in XDocument.Load(_filename).Root.Descendants("Rolle") select x.Attribute("Name").Value;
            namen.SequenceEqual(_rollen.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Rolle> _rollen;
        static string _filename;
        static RollenXmlPersister _sut;
    }


    internal class Wenn_Rollen_geladen_werden
    {
        Establish context = () =>
        {
            _rollen = new List<Rolle>
            {
                new Rolle("Captain"),
                new Rolle("Lieutnant"),
            };
            _filename = FileNamer.GetFilenameFor(RollenXmlPersister.ListName);
            new XmlFile().WithLine("<?xml version='1.0' encoding='utf-8'?>")
                .WithLine("<PersonalPlanung xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>")
                .WithLine("<Rollen>")
                .WithContent("<Rolle Name='").WithContent(_rollen[0].Name).WithLine("' />")
                .WithContent("<Rolle Name='").WithContent(_rollen[1].Name).WithLine("' />")
                .WithLine("</Rollen> ")
                .WithLine("</PersonalPlanung>")
                .AsFile(_filename);
            _sut = new RollenXmlPersister();
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
            _elements.Select(x=>x.Name).SequenceEqual(_rollen.Select(x => x.Name)).ShouldBeTrue();
        };

        static List<Rolle> _rollen;
        static string _filename;
        static RollenXmlPersister _sut;
        static IEnumerable<Rolle> _elements;
    }

}