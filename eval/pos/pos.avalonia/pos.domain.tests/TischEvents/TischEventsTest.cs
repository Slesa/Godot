using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.data;

namespace pos.domain.tests.TischEvents
{
    public class Wenn_eine_order_replayed_wird
    {
        [SetUp]
        public void Setup()
        {
            var store = new TischEventStore(42, 1);
            _anzahl = 3u;
            _plu = 13;
            _preis = 33;
            store.AddEvent( new ArtikelBestelltEvent(_anzahl, _plu, _preis) );
            _tisch = store.Replay();
        }

        [Test]
        public void Soll_die_Bestellung_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(1);

        [Test]
        public void Soll_die_Anzahl_übernommen_werden() => _tisch.Events.Cast<ArtikelBestelltEvent>().FirstOrDefault().Anzahl.Should().Be(_anzahl);
        
        domain.Tisch _tisch;
        uint _anzahl;
        uint _plu;
        decimal _preis;
  }
}