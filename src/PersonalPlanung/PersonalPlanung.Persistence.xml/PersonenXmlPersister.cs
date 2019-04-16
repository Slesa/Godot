using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class PersonDto
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Vorname { get; set; }
        [XmlAttribute]
        public decimal MinutenSatz { get; set; }
        [XmlAttribute]
        public string StatusName { get; set; }
//        public List<Rolle> EinsetzbarAls { get; set; } = new List<Rolle>();
    }

    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(PersonDto))]
    public class PersonenDto
    {
        [XmlArray("Personen")]
        [XmlArrayItem("Person")]
        public List<PersonDto> Personen { get; set; }
    }

    public class PersonenXmlPersister : XmlPersister<Person, PersonenDto>
    {
        public const string ListName = "Personen";

        public PersonenXmlPersister()
            : base(ListName)
        { }

        public override object GetDto(IEnumerable<Person> listData)
        {
            return new PersonenDto { Personen = listData.Select(x => 
                new PersonDto
                {
                    Name = x.Name,
                    Vorname = x.Vorname,
                    MinutenSatz = x.MinutenSatz,
                    StatusName = x.Status.Name,
                }).ToList() };
        }

        public override IEnumerable<Person> GetOrigin(PersonenDto dto)
        {
            return dto.Personen.Select(x => 
                new Person {
                    Name = x.Name,
                    Vorname = x.Vorname,
                    MinutenSatz = x.MinutenSatz,
                    Status = new Status(x.StatusName),
                });
        }
    }
}