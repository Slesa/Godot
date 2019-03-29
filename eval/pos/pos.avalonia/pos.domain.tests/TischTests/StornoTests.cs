using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using pos.data;
using pos.domain.Tische;

namespace pos.domain.tests.TischTests
{
    internal class Wenn_von_einem_Tisch_storniert_wird
    {
        [SetUp]
        public void Setup()
        {
            _kellner = new TischKellner(13, "TestKellner");
            _tisch = new Tisch(TischNr.TestDefault);
            _artikel = new Artikel { Preis = 33, Plu = 4711 };
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, 3, _artikel));
            _tisch.StornierePlu(new StornierePluCommand(_kellner, 2, 4711));
        }

        [Test]
        public void Sollen_die_Events_auf_dem_Tisch_sein() => _tisch.Events.Count().Should().Be(3);
        [Test]
        public void Soll_die_Stornierung_auf_dem_Tisch_sein() => _tisch.Events.FilterEvents<ArtikelStorniertEvent>().Count().Should().Be(1);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel.Preis);
        [Test]
        public void Soll_der_Storno_den_Kellner_übernommen_haben() =>
            _tisch.Events
                .FilterEvents<ArtikelStorniertEvent>().FirstOrDefault().Kellner.Should().Be(_kellner);

        Tisch _tisch;
        Artikel _artikel;
        TischKellner _kellner;
    }

    internal class Wenn_eine_PLU_storniert_wird_die_nicht_auf_dem_Tisch_ist
    {
        [SetUp]
        public void Setup()
        {
            _tisch = new Tisch(TischNr.TestDefault);
            try
            {
                _tisch.StornierePlu(new StornierePluCommand(TischKellner.Null, 2, 4711));
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

        Tisch _tisch;
        Exception _exception;
    }

    internal class Wenn_eine_PLU_storniert_wird_die_mehrere_Posten_auf_dem_Tisch_umfasst
    {
        [SetUp]
        public void Setup()
        {
            _artikel = new Artikel { Preis = 33, Plu = 4711 };
            _tisch = new Tisch(TischNr.TestDefault);
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, 2, _artikel));
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, 2, _artikel));
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, 2, _artikel));
            _tisch.StornierePlu(new StornierePluCommand(TischKellner.Null, 5, _artikel.Plu));
        }

        [Test]
        public void Sollen_StornoEvents_einzeln_auf_dem_Tisch_sein() => 
            _tisch.Events.FilterEvents<ArtikelStorniertEvent>().Count().Should().Be(3);
        [Test]
        public void Sollen_BestelltEvents_immer_noch_auf_dem_Tisch_sein() =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Count().Should().Be(3);
        [Test]
        public void Soll_der_Tischbetrag_angepasst_sein() => _tisch.Betrag.Should().Be(_artikel.Preis);
        [Test]
        public void Soll_der_Storno_den_Kellner_übernommen_haben() =>
            _tisch.Events.FilterEvents<ArtikelStorniertEvent>().First().Kellner.Should().Be(TischKellner.Null);

        Tisch _tisch;
        Artikel _artikel;
    }
}