using System.Collections.Generic;
using System.Linq;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    public class RollenXmlPersister : XmlPersister<Rolle, RollenDto>
    {
        public const string ListName = "Rollen";

        public RollenXmlPersister()
            : base(ListName)
        {}

        public override object GetDto(IEnumerable<Rolle> listData)
        {
            return new RollenDto { Rollen = listData.Select(x => new RolleDto { Name = x.Name}).ToList() };
        }

        public override IEnumerable<Rolle> GetOrigin(RollenDto dto)
        {
            return dto.Rollen.Select(x => new Rolle(x.Name));
        }
    }
}