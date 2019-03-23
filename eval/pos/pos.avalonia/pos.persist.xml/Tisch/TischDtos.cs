using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace pos.persist.xml.Tisch
{
    public class TischEintragDto
    {
        public uint Id { get; set; }
        public DateTime OccurredOn { get; set; }
    }

    [XmlRoot(ElementName = "Bestellung")]
    public class ArtikelBestelltDto : TischEintragDto
    {
        public uint Anzahl { get; set; }
        public uint Plu { get; set; }
        public decimal Preis { get; set; }
    }

    [XmlRoot(ElementName = "Storno")]
    public class ArtikelStorniertDto : TischEintragDto
    {
        public uint Anzahl { get; set; }
        public uint Bestellung { get; set; }
        public decimal Betrag { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "Tisch")]
    [XmlInclude(typeof(TischEintragDto))]
    [XmlInclude(typeof(ArtikelBestelltDto))]
    [XmlInclude(typeof(ArtikelStorniertDto))]
    public class TischDto
    {
        List<TischEintragDto> _tischInhalt;
        [XmlArrayItem("TischInhalt")]
        public List<TischEintragDto> TischInhalt
        {
            get => _tischInhalt ?? (_tischInhalt = new List<TischEintragDto>());
            set => _tischInhalt = value;
        }
    }
}