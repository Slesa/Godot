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
        public decimal? MinutenSatz { get; set; }
        [XmlAttribute("MinutenSatz")]
        public string MinutenSatzString
        {
            get => MinutenSatz?.ToString(CultureInfo.InvariantCulture);
            set => MinutenSatz = decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        [XmlAttribute]
        public string BerufName { get; set; }

        [XmlArray]
        [XmlArrayItem("Rolle")]
        public List<RolleDto> EinsetzbarAls { get; set; }
    }

    public static class PersonDtoExtensions
    {
        public static Person ToOrigin(this PersonDto dto)
        {
            return new Person
            {
                Name = dto.Name, Vorname = dto.Vorname, Beruf = new Beruf(dto.BerufName),
                MinutenSatz = dto.MinutenSatz ?? 0m
            };
        }

        public static PersonDto FromOrigin(this PersonDto dto, Person person)
        {
            if( person!=null)
            {
                dto.Name = person.Name;
                if (!string.IsNullOrEmpty(person.Vorname)) dto.Vorname = person.Vorname;
                dto.BerufName = person.Beruf?.Name;
                if(person.MinutenSatz > 0m) dto.MinutenSatz = person.MinutenSatz;
            }
            return dto;
        }
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
            {
                var result = new PersonDto
                {
                    Name = x.Name,
                    Vorname = !string.IsNullOrEmpty(x.Vorname) ? x.Vorname : null,
                    BerufName = x.Beruf?.Name,
                    EinsetzbarAls = x.EinsetzbarAls.Select(y => new RolleDto {Name = y.Name}).ToList()
                };
                if (x.MinutenSatz != 0m) result.MinutenSatz = x.MinutenSatz;
                return result;
            }).ToList() };
        }

        public override IEnumerable<Person> GetOrigin(PersonenDto dto)
        {
            return dto.Personen.Select(x => 
                new Person {
                    Name = x.Name,
                    Vorname = x.Vorname,
                    MinutenSatz = x.MinutenSatz ?? 0m,
                    Beruf = new Beruf(x.BerufName),
                    EinsetzbarAls = x.EinsetzbarAls.Select(y => new Rolle(y.Name)).ToList()
                });
        }
    }
}