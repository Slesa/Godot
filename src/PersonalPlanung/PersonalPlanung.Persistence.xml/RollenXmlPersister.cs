using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class RolleDto
    {
        [XmlAttribute]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(RolleDto))]
    public class RollenDto
    {
        [XmlArray("Rollen")]
        [XmlArrayItem("Rolle")]
        public List<RolleDto> Rollen { get; set; }
    }

    public class RollenXmlPersister : XmlPersister<Rolle, RollenDto>, IRollePersister
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