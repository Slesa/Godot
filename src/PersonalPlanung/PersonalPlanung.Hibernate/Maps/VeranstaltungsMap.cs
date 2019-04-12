using FluentNHibernate.Mapping;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Hibernate.Maps
{
    public class VeranstaltungsMap : ClassMap<Veranstaltung>
    {
        public VeranstaltungsMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);
            Map(d => d.BeginntAm);
            Map(d => d.EndetAm);
            Version(d => d.Version);
        }
    }
}