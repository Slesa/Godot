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
    [Subject(typeof(Tisch))]
    internal class Wenn_ein_Tisch_erzeugt_wird
    {
        Establish context = () => _tischNr = new TischNr(42u, 1);
        Because of = () => _tisch = new Tisch(_tischNr);

        It soll_die_TischNr_gesetzt_sein = () => _tisch.TischNr.ShouldEqual(_tischNr);
        It soll_keine_Events_haben = () => _tisch.Events.ShouldBeEmpty();
        It soll_die_Runde_0_sein = () => _tisch.AktuelleRunde.ShouldEqual(0u);

        static Tisch _tisch;
        static TischNr _tischNr;
    }

    [Subject(typeof(Tisch))]
    internal class Die_erste_Aktion_auf_einem_Tisch_legt_einen_TischErzeugtEvent_an
    {
        Establish context = () => _tisch = new Tisch(TischNr.TestDefault);
        Because of = () => _tisch.BestelleArtikel(new BestelleArtikelCommand(TischKellner.Null, 1, new Artikel { Preis = 33, Plu = 13 }));

        It sollen_zwei_Events_auf_dem_Tisch_sein = () => _tisch.Events.Count().ShouldEqual(2);
        It soll_der_erste_Event_die_Erzeugung_sein = () => _tisch.Events.First().ShouldBeOfExactType<TischErzeugtEvent>();

        static Tisch _tisch;
    }
}
