using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class StandortDto
    {
        [XmlAttribute]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(StandortDto))]
    public class StandorteDto
    {
        [XmlArray("Standorte")]
        [XmlArrayItem("Standort")]
        public List<StandortDto> Standorte { get; set; }
    }

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