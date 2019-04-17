using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class BerufDto
    {
        [XmlAttribute]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(BerufDto))]
    public class BerufeDto
    {
        [XmlArray("Berufe")]
        [XmlArrayItem("Beruf")]
        public List<BerufDto> Berufe { get; set; }
    }

    public class BerufeXmlPersister : XmlPersister<Beruf, BerufeDto>, IBerufPersister
    {
        public const string ListName = "Berufe";

        public BerufeXmlPersister()
            : base(ListName)
        { }

        public override object GetDto(IEnumerable<Beruf> listData)
        {
            return new BerufeDto { Berufe = listData.Select(x => new BerufDto { Name = x.Name }).ToList() };
        }

        public override IEnumerable<Beruf> GetOrigin(BerufeDto dto)
        {
            return dto.Berufe.Select(x => new Beruf(x.Name));
        }
    }
}