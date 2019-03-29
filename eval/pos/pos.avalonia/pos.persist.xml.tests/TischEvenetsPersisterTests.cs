using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.domain;
using pos.domain.Tische;

// ReSharper disable once InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace pos.persist.xml.tests
{
    internal class Wenn_ein_Tisch_gespeichert_wird
    {
        [SetUp]
        public void Setup()
        {
            _es = new domain.TischEventStore(new TischNr(42, 1));
            var bestellung = new ArtikelBestelltEvent(TischKellner.Null, 3, 4711, 3.33M);
            _es.AddEvent(bestellung);
            var storno = new ArtikelStorniertEvent(TischKellner.Null, 2, bestellung);
            _es.AddEvent(storno);
            new TischEventsXmlPersister().Speichern(_es);
        }

        [Test]
        public void Soll_die_Datei_generiert_werden() => File.Exists("T42_1.0").Should().BeTrue();

        [Test]
        public void Soll_wieder_einlesbar_sein()
        {
            var es = new TischEventsXmlPersister().Laden(new TischNr(42, 1));
            es.TischNr.Should().Be(_es.TischNr);

            var x = 0;
            foreach (var src in es.Events)
            {
                var dst = _es.Events.ToArray()[x++];
                src.Id.Should().Be(dst.Id);
                src.OccurredOn.Should().Be(dst.OccurredOn);
            }
            ///es.Events.Should().AllBeEquivalentTo(_es.Events);
        }

        TischEventStore _es;
    }
}
