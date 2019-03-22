using System;
using Machine.Specifications;
using pos.domain;

namespace pos.specs
{
    [Subject(typeof(Tisch))]
    internal class Wenn_ein_Tisch_erzeugt_wird
    {
        Establish context = () => _tisch = new Tisch(42, 1);

        It soll_die_TischNr_gesetzt_sein = () => _tisch.TischNr.ShouldEqual(42u);

        static Tisch _tisch;
    }
}
