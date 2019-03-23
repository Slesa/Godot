using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.data;
// ReSharper disable once InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace pos.domain.tests.Tisch
{
    internal class Wenn_ein_Tisch_gespeichert_wird
    {
    private TischEventStore _es;

    [SetUp]
        public void Setup()
        {
          System.Console.Write("a");
            _es = new domain.TischEventStore(42, 1);
          System.Console.Write("b");
            var bestellung = new ArtikelBestelltEvent(42, 3, 4711, 3.33M);
          System.Console.Write("c");
            _es.Events.Append(bestellung);
          System.Console.Write("d");
            var storno = new ArtikelStorniertEvent(1, 2, bestellung);
          System.Console.Write("e");
            _es.Events.Append(storno);
          System.Console.Write("f");
            new TischEventsXmlPersister().Speichern(_es);
          System.Console.Write("g");
        }

        [Test]
        public void Soll_die_Datei_generiert_werden() => File.Exists("T32_01.0").Should().BeTrue();
    }
}
