using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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
}