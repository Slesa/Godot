using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.data;

namespace pos.domain.tests.Tisch
{
    internal class Wenn_von_einem_Tisch_storniert_wird
    {
        [SetUp]
        public void Setup()
        {
            _tisch = new domain.Tisch(42, 1);
            _artikel = new Artikel { Preis = 33, Plu = 4711 };
            _tisch.BestelleArtikel(new BestelleArtikelCommand(3, _artikel));
            _tisch.StornierePlu(new StornierePluCommand(2, 4711));
        }

        [Test]
        public void Soll_die_Stornierung_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(2);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel.Preis);

        domain.Tisch _tisch;
        Artikel _artikel;
    }

    internal class Wenn_eine_PLU_storniert_wird_die_nicht_auf_dem_Tisch_ist
    {
        [SetUp]
        public void Setup()
        {
            _tisch = new domain.Tisch(42, 1);
            try
            {
                _tisch.StornierePlu(new StornierePluCommand(2, 4711));
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Test]
        public void Soll_nichts_passieren() => _exception.Should().BeNull();

        [Test]
        public void Soll_kein_Event_registriert_werden() => _tisch.Events.Count().Should().Be(0);

        domain.Tisch _tisch;
        Exception _exception;
    }

    internal class Wenn_eine_PLU_storniert_wird_die_mehrere_Posten_auf_dem_Tisch_umfasst
    {
        [SetUp]
        public void Setup()
        {
            _artikel = new Artikel { Preis = 33, Plu = 4711 };
            _tisch = new domain.Tisch(42, 1);
            _tisch.BestelleArtikel(new BestelleArtikelCommand(2, _artikel));
            _tisch.BestelleArtikel(new BestelleArtikelCommand(2, _artikel));
            _tisch.BestelleArtikel(new BestelleArtikelCommand(2, _artikel));
            _tisch.StornierePlu(new StornierePluCommand(5, _artikel.Plu));
        }

        [Test]
        public void Sollen_die_Events_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(6);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel.Preis);

        domain.Tisch _tisch;
        Artikel _artikel;
    }
}