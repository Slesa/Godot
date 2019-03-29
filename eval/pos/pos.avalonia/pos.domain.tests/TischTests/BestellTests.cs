#if NUNIT
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.data;
using pos.domain.Tische;

// ReSharper disable once InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace pos.domain.tests.TischTests
{
    internal class Wenn_auf_einen_Tisch_bestellt_wird
    {
        [SetUp]
        public void Setup()
        {
            _tisch = new Tisch(TischNr.TestDefault);
            _anzahl = 3u;
            _artikel = new Artikel { Preis = 33, Plu = 13 };
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, _anzahl, _artikel));
        }

        [Test]
        public void Sollen_die_Events_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(2);
        [Test]
        public void Soll_die_Bestellung_auf_dem_Tisch_sein() => _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Count().Should().Be(1);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel.Preis * _anzahl);
        [Test]
        public void Soll_die_AktuelleRunde_angepasst_sein() => _tisch.AktuelleRunde.Should().Be(1);
        [Test]
        public void Soll_die_Bestellung_den_Kellner_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Kellner.Should().Be(TischKellner.Null);
        [Test]
        public void Soll_die_Bestellung_die_Anzahl_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Anzahl.Should().Be(3);
        [Test]
        public void Soll_die_Bestellung_die_Plu_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Plu.Should().Be(13);

        Tisch _tisch;
        Artikel _artikel;
        uint _anzahl;
    }

    internal class Wenn_zwei_Artikel_auf_einen_Tisch_bestellt_werden
    {
        [SetUp]
        public void Setup()
        {
            _tisch = new Tisch(TischNr.TestDefault);
            _anzahl1 = 3u;
            _artikel1 = new Artikel { Preis = 33, Plu = 13 };
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, _anzahl1, _artikel1));
        }

        [Test]
        public void Sollen_die_Events_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(2);
        [Test]
        public void Soll_die_Bestellung_auf_dem_Tisch_sein() => _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Count().Should().Be(1);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel1.Preis * _anzahl1);
        [Test]
        public void Soll_die_AktuelleRunde_angepasst_sein() => _tisch.AktuelleRunde.Should().Be(1);
        [Test]
        public void Soll_die_Bestellung_den_Kellner_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Kellner.Should().Be(TischKellner.Null);
        [Test]
        public void Soll_die_Bestellung_die_Anzahl_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Anzahl.Should().Be(3);
        [Test]
        public void Soll_die_Bestellung_die_Plu_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Plu.Should().Be(13);

        Tisch _tisch;
        Artikel _artikel1;
        uint _anzahl1;
    }

}
#endif
