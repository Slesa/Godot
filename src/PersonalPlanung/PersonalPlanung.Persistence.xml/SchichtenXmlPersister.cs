using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class SchichtDto
    {
        [XmlElement]
        public AufgabeDto Aufgabe { get; set; }

        [XmlElement]
        public PersonDto Person { get; set; }

        [XmlElement]
        public VeranstaltungsDto Veranstaltung { get; set; }
    }


    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(VeranstaltungsDto))]
    public class SchichtenDto
    {
        [XmlArray("Schichten")]
        [XmlArrayItem("Schicht")]
        public List<SchichtDto> Schichten { get; set; }
    }

    public class SchichtenXmlPersister : XmlPersister<Schicht, SchichtenDto>, ISchichtPersister
    {
        public const string ListName = "Schichten";

        public SchichtenXmlPersister() 
            : base(ListName)
        {}

        public override object GetDto(IEnumerable<Schicht> listData)
        {
            return new SchichtenDto
            {
                Schichten = listData.Select(x => new SchichtDto
                    {
                        Aufgabe = new AufgabeDto()
                        {
                            Beginn = x.Aufgabe.Beginn, Ende = x.Aufgabe.Ende, RollenName = x.Aufgabe.Rolle.Name,
                            StandortName = x.Aufgabe.Standort.Name
                        },
                        Person = new PersonDto().FromOrigin(x.Person),
                        Veranstaltung = new VeranstaltungsDto()
                        {
                            Name = x.Veranstaltung.Name,
                            BeginntAm = x.Veranstaltung.BeginntAm,
                            EndetAm = x.Veranstaltung.EndetAm
                        }
                    }
                ).ToList()
            };
        }

        public override IEnumerable<Schicht> GetOrigin(SchichtenDto dto)
        {
            return dto.Schichten.Select(x =>
                new Schicht
                {
                    Aufgabe = new Aufgabe(x.Aufgabe.Beginn, x.Aufgabe.Ende, new Rolle(x.Aufgabe.RollenName), new Standort(x.Aufgabe.StandortName)),
                    Person = x.Person.ToOrigin(),
                    Veranstaltung = new Veranstaltung(x.Veranstaltung.Name, x.Veranstaltung.BeginntAm, x.Veranstaltung.EndetAm)
                });

        }
    }
}