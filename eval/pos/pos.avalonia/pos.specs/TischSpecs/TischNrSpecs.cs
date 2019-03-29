using System;
using Machine.Specifications;
using pos.domain.Tische;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
#pragma warning disable 169

namespace pos.domain.specs.TischSpecs
{
    [Subject(typeof(TischNr))]
    internal class Wenn_eine_TischNr_erzeugt_wird
    {
        Because of = () => _tischNr = new TischNr(43, 1);

        It soll_tisch_setzen = () => _tischNr.Tisch.ShouldEqual(43u);
        It soll_partei_setzen = () => _tischNr.Partei.ShouldEqual(1u);

        static TischNr _tischNr;
    }

    [Subject(typeof(TischNr))]
    internal class Wenn_eine_TischNr_mit_einem_ungültigen_Tisch_erzeugt_wird
    {
        Because of = () => _exception = Catch.Exception(() => new TischNr(0, 1));

        It soll_die_TischNr_nicht_angelegt_werden = () => _exception.ShouldBeOfExactType<ArgumentException>();

        static Exception _exception;
    }


    [Subject(typeof(TischNr))]
    internal class Wenn_eine_TischNr_mit_einer_ungültigen_Partei_erzeugt_wird
    {
        Because of = () => _exception = Catch.Exception(() => new TischNr(1, 0));

        It soll_die_TischNr_nicht_angelegt_werden = () => _exception.ShouldBeOfExactType<ArgumentException>();

        static Exception _exception;
    }
}