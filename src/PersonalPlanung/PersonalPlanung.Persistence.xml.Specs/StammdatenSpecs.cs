using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Machine.Specifications;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml.Specs
{
    internal class Wenn_Stammdaten_gespeichert_werden
    {
        Establish context = () =>
        {
            _rollen = new List<Rolle>
            {
                new Rolle("Captain"),
                new Rolle("Lieutnant"),
            };
        };

        Cleanup teardown = () =>
        {
            //File.Delete();
        };

        Because of = () =>
        {

        };

        It should = () =>
        {
            XDocument xDoc = XDocument.Load();
        }
        static List<Rolle> _rollen;
    }
}