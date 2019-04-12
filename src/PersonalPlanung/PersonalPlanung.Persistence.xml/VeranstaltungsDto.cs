using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
 /*   public class VeranstaltungsDto
    {
        public string Name { get; set; }
        public DateTime BeginntAm { get; set; }
        public DateTime EndetAm { get; set; }

//        public List<Posten> Posten { get; } = new List<Posten>();
    } */

    [Serializable]
    [XmlRoot(ElementName = "Veranstaltungen")]
    [XmlInclude(typeof(Veranstaltung))]
    public class VeranstaltungenDto
    {
        [XmlArray("Veranstaltung")]
        [XmlArrayItem("EineVeranstaltung")]
        public List<VeranstaltungenDto> Veranstaltungen { get; set; }
    }
}