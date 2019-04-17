using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class AufgabeDto
    {
        [XmlAttribute]
        public DateTime Beginn { get; set; }
        [XmlAttribute]
        public DateTime Ende { get; set; }
        [XmlAttribute]
        public string RollenName { get; set; }
        [XmlAttribute]
        public string StandortName { get; set; }
    }


    [Serializable]
    public class VeranstaltungsDto
    {
        public const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public DateTime BeginntAm { get; set; }

        [XmlAttribute("BeginntAm")]
        public string BeginntAmString
        {
            get => BeginntAm.ToString(DateFormat);
            set => BeginntAm = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime EndetAm { get; set; }

        [XmlAttribute("EndetAm")]
        public string EndetAmString
        {
            get => EndetAm.ToString(DateFormat);
            set => EndetAm = DateTime.Parse(value);
        }

        [XmlArray("Aufgaben")]
        [XmlArrayItem("Aufgabe")]
        public List<AufgabeDto> Aufgaben { get; set; }
    }


    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(VeranstaltungsDto))]
    public class VeranstaltungenDto
    {
        [XmlArray("Veranstaltungen")]
        [XmlArrayItem("Veranstaltung")]
        public List<VeranstaltungsDto> Veranstaltungen { get; set; }
    }

    public class VeranstaltungenXmlPersister : XmlPersister<Veranstaltung, VeranstaltungenDto>, IVeranstaltungPersister
    {
        public const string ListName = "Veranstaltungen";

        public VeranstaltungenXmlPersister()
            : base(ListName)
        { }

        public override object GetDto(IEnumerable<Veranstaltung> listData)
        {
            return new VeranstaltungenDto { Veranstaltungen = listData.Select(x => 
                new VeranstaltungsDto
                {
                    Name = x.Name,
                    BeginntAm = x.BeginntAm,
                    EndetAm = x.EndetAm,
                    Aufgaben = x.Aufgaben.Select(y => new AufgabeDto
                    {
                        Beginn = y.Beginn,
                        Ende = y.Ende,
                        RollenName = y.Rolle.Name,
                        StandortName = y.Standort.Name
                    }).ToList()
                }).ToList() };
        }

        public override IEnumerable<Veranstaltung> GetOrigin(VeranstaltungenDto dto)
        {
            return dto.Veranstaltungen.Select(x => 
                new Veranstaltung(x.Name, x.BeginntAm, x.EndetAm)
                    { Aufgaben = x.Aufgaben.Select(y => new Aufgabe(y.Beginn, y.Ende, new Rolle(y.RollenName), new Standort(y.StandortName) )).ToList() });
        }
    }
}