using FluentAssertions;
using NUnit.Framework;

namespace pos.domain.tests.Tisch
{
    internal class Wenn_ein_Tisch_erzeugt_wird
    {
        [SetUp]
        public void Setup() => _tisch = new domain.Tisch(42, 1);

        [Test]
        public void Soll_die_TischNr_gesetzt_sein() => _tisch.TischNr.Should().Be(42u);
        [Test]
        public void Soll_die_ParteiNr_gesetzt_sein() => _tisch.ParteiNr.Should().Be(1u);
        [Test]
        public void Soll_keine_aktionen_haben() => _tisch.Events.Should().BeEmpty();

        domain.Tisch _tisch;
    }
}