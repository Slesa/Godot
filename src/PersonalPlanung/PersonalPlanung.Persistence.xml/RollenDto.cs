using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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
}