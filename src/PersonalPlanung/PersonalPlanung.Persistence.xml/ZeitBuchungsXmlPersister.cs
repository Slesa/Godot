using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    [Serializable]
    public class ZeitBuchungsDto
    {
        public const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        [XmlAttribute]
        public uint Minuten { get; set; }
        [XmlAttribute]
        public string RollenName { get; set; }

        [XmlIgnore]
        public decimal? MinutenSatz { get; set; }
        [XmlAttribute("MinutenSatz")]
        public string MinutenSatzString
        {
            get => MinutenSatz?.ToString(CultureInfo.InvariantCulture);
            set => MinutenSatz = decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        [XmlIgnore]
        public DateTime Wann { get; set; }
        [XmlAttribute("Wann")]
        public string WannString
        {
            get => Wann.ToString(DateFormat);
            set => Wann = DateTime.Parse(value);
        }

        [XmlAttribute]
        public bool Verbucht { get; set; }
        [XmlElement]
        public PersonDto Person { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "PersonalPlanung")]
    [XmlInclude(typeof(ZeitBuchungsDto))]
    public class ZeitBuchungenDto
    {
        [XmlArray("Buchungen")]
        [XmlArrayItem("Buchung")]
        public List<ZeitBuchungsDto> Buchungen { get; set; }
    }


    public class ZeitBuchungsXmlPersister : XmlPersister<ZeitBuchung, ZeitBuchungenDto>, IZeitBuchungsPersister
    {
        public const string ListName = "Buchungen";

        public ZeitBuchungsXmlPersister()
            : base(ListName)
        {
        }

        public override object GetDto(IEnumerable<ZeitBuchung> listData)
        {
            return new ZeitBuchungenDto()
            {
                Buchungen = listData.Select(x =>
                {
                    var result = new ZeitBuchungsDto
                    {
                        Minuten = x.Minuten,
                        RollenName = x.Rolle.Name,
                        Wann = x.Wann,
                        Verbucht = x.Verbucht,
                        Person = new PersonDto().FromOrigin(x.Person)
                    };
                    if (x.MinutenSatz != 0m) result.MinutenSatz = x.MinutenSatz;
                    return result;
                }).ToList()
            };
        }

        public override IEnumerable<ZeitBuchung> GetOrigin(ZeitBuchungenDto dto)
        {
            return dto.Buchungen.Select(x =>
                new ZeitBuchung()
                {
                    Minuten = x.Minuten,
                    MinutenSatz = x.MinutenSatz ?? 0m,
                    Rolle = new Rolle(x.RollenName),
                    Wann = x.Wann,
                    Verbucht = x.Verbucht,
                    Person = x.Person.ToOrigin(),
                }
            );
        }
    }
}