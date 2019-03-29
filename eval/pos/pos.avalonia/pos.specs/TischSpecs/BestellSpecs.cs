using System.Linq;
using Machine.Specifications;
using pos.data;
using pos.domain.Tische;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
#pragma warning disable 169

namespace pos.domain.specs.TischSpecs
{
    internal class Wenn_auf_einen_Tisch_bestellt_wird
    {
        Establish context = () =>
        {
            _tisch = new Tisch(TischNr.TestDefault);
            _anzahl = 3u;
            _artikel = new Artikel {Preis = 33, Plu = 13};
        };

        Because of = () => _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, _anzahl, _artikel));

        It sollen_die_Events_auf_dem_Tisch_sein = () => _tisch.Events.Count().ShouldEqual(2);
        It soll_die_Bestellung_auf_dem_Tisch_sein = () => 
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Count().ShouldEqual(1);
        It soll_der_Tischbetrag_angepasst_sein = () => 
            _tisch.Betrag.ShouldEqual(_artikel.Preis * _anzahl);
        It soll_die_AktuelleRunde_angepasst_sein = () => 
            _tisch.AktuelleRunde.ShouldEqual(1u);
        It soll_die_Bestellung_den_Kellner_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Kellner.ShouldEqual(TischKellner.Null);
        It soll_die_Bestellung_die_Anzahl_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Anzahl.ShouldEqual(_anzahl);
        It soll_die_Bestellung_die_Plu_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Plu.ShouldEqual(_artikel.Plu);

        static Tisch _tisch;
        static Artikel _artikel;
        static uint _anzahl;
    }



    internal class Wenn_zwei_Artikel_auf_einen_Tisch_bestellt_werden
    {
        Establish context = () =>
        {
            _tisch = new Tisch(TischNr.TestDefault);
            _anzahl1 = 3u;
            _artikel1 = new Artikel {Preis = 33, Plu = 13};
            _anzahl2 = 2u;
            _artikel2 = new Artikel {Preis = 12, Plu = 11};
        };
        Because of = () =>
        {
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, _anzahl1, _artikel1));
            _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, _anzahl2, _artikel2));
        };

        It sollen_die_Events_auf_dem_Tisch_sein = () => _tisch.Events.Count().ShouldEqual(3);
        It soll_die_Bestellung_auf_dem_Tisch_sein = () => 
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Count().ShouldEqual(2);
        It soll_der_Tischbetrag_angepasst_sein = () => 
            _tisch.Betrag.ShouldEqual(_artikel1.Preis * _anzahl1 + _artikel2.Preis * _anzahl2);
        It soll_die_AktuelleRunde_gleich_bleiben = () => 
            _tisch.AktuelleRunde.ShouldEqual(1u);
        It soll_die_Bestellung_den_Kellner_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Kellner.ShouldEqual(TischKellner.Null);
        It soll_die_erste_Bestellung_in_Anzahl_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Anzahl.ShouldEqual(_anzahl1);
        It soll_die_erste_Bestellung_in_Plu_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().First().Plu.ShouldEqual(_artikel1.Plu);
        It soll_die_zweite_Bestellung_in_Anzahl_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Skip(1).First().Anzahl.ShouldEqual(_anzahl2);
        It soll_die_zweite_Bestellung_in_Plu_übernommen_haben = () =>
            _tisch.Events.FilterEvents<ArtikelBestelltEvent>().Skip(1).First().Plu.ShouldEqual(_artikel2.Plu);

        static Tisch _tisch;
        static Artikel _artikel1;
        static uint _anzahl1;
        static Artikel _artikel2;
        static uint _anzahl2;
    }

}