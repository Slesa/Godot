using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.data;
// ReSharper disable once InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace pos.domain.tests.Tisch
{
    internal class Wenn_auf_einen_Tisch_bestellt_wird
    {
        [SetUp]
        public void Setup()
        {
            _tisch = new domain.Tisch(42, 1);
            _anzahl = 3u;
            _artikel = new Artikel { Preis = 33, Plu = 13 };
            _tisch.BestelleArtikel(new BestelleArtikelCommand(_anzahl, _artikel));
        }

        [Test]
        public void Soll_die_Bestellung_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(1);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel.Preis * _anzahl);
        [Test]
        public void Soll_die_Bestellung_die_Anzahl_übernommen_haben() =>
            _tisch.Events.Cast<ArtikelBestelltEvent>().FirstOrDefault().Anzahl.Should().Be(3);
        [Test]
        public void Soll_die_Bestellung_die_Plu_übernommen_haben() =>
            _tisch.Events.Cast<ArtikelBestelltEvent>().FirstOrDefault().Plu.Should().Be(13);

        domain.Tisch _tisch;
        Artikel _artikel;
        uint _anzahl;
    }

}