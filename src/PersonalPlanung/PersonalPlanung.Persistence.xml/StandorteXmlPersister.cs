using System.Collections.Generic;
using System.Linq;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    public class StandorteXmlPersister : XmlPersister<Standort, StandorteDto>
    {
        public const string ListName = "Standorte";

        public StandorteXmlPersister()
            : base(ListName)
        { }

        public override object GetDto(IEnumerable<Standort> listData)
        {
            return new StandorteDto { Standorte = listData.Select(x => new StandortDto { Name = x.Name }).ToList() };
        }

        public override IEnumerable<Standort> GetOrigin(StandorteDto dto)
        {
            return dto.Standorte.Select(x => new Standort(x.Name));
        }
    }
}