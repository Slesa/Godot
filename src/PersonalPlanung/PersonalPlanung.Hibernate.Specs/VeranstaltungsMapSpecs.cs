using System;
using FluentNHibernate.Testing;
using Hibernate.Specs.Common;
using Machine.Specifications;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Hibernate.Maps;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace PersonalPlanung.Hibernate.Specs
{
    [Subject(typeof(VeranstaltungsMap))]
    internal class When_checking_persistence_of_veranstaltung : InMemoryDatabaseSpecs<VeranstaltungsMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<Veranstaltung>(Session);
            var veranstaltung = new Veranstaltung("Eine Veranstaltung", DateTime.Now, DateTime.Now.AddDays(1));
            spec.TransactionalSave(veranstaltung);
            _check = spec
                .CheckProperty(d => d.Name, "Eine Veranstaltung")
                .CheckProperty(d => d.BeginntAm, DateTime.Now)
                .CheckProperty(d => d.EndetAm, DateTime.Now.AddDays(1))
                .CheckProperty(d => d.Version, 1);
        };

        It should_be_verfied = () => _check.VerifyTheMappings();

        static PersistenceSpecification<Veranstaltung> _check;
    }

    [Subject(typeof(VeranstaltungsMap))]
    internal class When_saving_one_veranstaltung : InMemoryDatabaseSpecs<VeranstaltungsMap>
    {
        Establish context = () =>
        {
            _beginn = new DateTime(1994, 4, 27);
            _ende = new DateTime(1994, 4, 28);
            var veranstaltung = new Veranstaltung("Eine Veranstaltung", _beginn, _ende);
            _id = (int)Session.Save(veranstaltung);
        };
        Because of = () => _veranstaltung = Session.Load<Veranstaltung>(_id);

        It should_save_the_object = () => _id.ShouldNotEqual(0);
        It should_save_the_name = () => _veranstaltung.Name.ShouldEqual("Eine Veranstaltung");
        It should_save_start_date = () => _veranstaltung.BeginntAm.ShouldEqual(_beginn);
        It should_save_end_date = () => _veranstaltung.EndetAm.ShouldEqual(_ende);
        It should_save_version = () => _veranstaltung.Version.ShouldEqual(1);

        static int _id;
        static Veranstaltung _veranstaltung;
        static DateTime _beginn;
        static DateTime _ende;
    }
}