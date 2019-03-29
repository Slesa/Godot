#if NUNIT
using FluentAssertions;
using NUnit.Framework;
using pos.domain.Tische;

// ReSharper disable once InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace pos.domain.tests.TischTests
{
    internal class Wenn_ein_Tisch_erzeugt_wird
    {
        [SetUp]
        public void Setup()
        {
            _tischNr = new TischNr(42, 1);
            _tisch = new Tisch(_tischNr);
        }

        [Test]
        public void Soll_die_TischNr_gesetzt_sein() => _tisch.TischNr.Should().Be(_tischNr);
        [Test]
        public void Soll_keine_aktionen_haben() => _tisch.Events.Should().BeEmpty();

        Tisch _tisch;
        TischNr _tischNr;
    }
}
#endif