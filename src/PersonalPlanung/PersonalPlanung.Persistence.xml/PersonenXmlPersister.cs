using System;
using System.Collections.Generic;
using System.Globalization;
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

        [XmlIgnore]
        public decimal MinutenSatz { get; set; }

        [XmlAttribute("MinutenSatz")]
        public string MinutenSatzString
        {
            get => MinutenSatz.ToString(CultureInfo.InvariantCulture);
            set => MinutenSatz = decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        [XmlAttribute]
        public string BerufName { get; set; }

        [XmlArray]
        [XmlArrayItem("Rolle")]
        public List<RolleDto> EinsetzbarAls { get; set; }
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

    public class PersonenXmlPersister : XmlPersister<Person, PersonenDto>, IPersonPersister
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
                    BerufName = x.Beruf?.Name,
                    EinsetzbarAls = x.EinsetzbarAls.Select(y => new RolleDto { Name = y.Name }).ToList()
                }).ToList() };
        }

        public override IEnumerable<Person> GetOrigin(PersonenDto dto)
        {
            return dto.Personen.Select(x => 
                new Person {
                    Name = x.Name,
                    Vorname = x.Vorname,
                    MinutenSatz = x.MinutenSatz,
                    Beruf = new Beruf(x.BerufName),
                    EinsetzbarAls = x.EinsetzbarAls.Select(y => new Rolle(y.Name)).ToList()
                });
        }
    }
}