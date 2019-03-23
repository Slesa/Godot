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

    public class ArtikelBestelltDto : TischEintragDto
    {
        public uint Anzahl { get; set; }
        public uint Plu { get; set; }
        public decimal Preis { get; set; }
    }

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
        [XmlArray("TischInhalt")]
        [XmlArrayItem("Eintrag")]
        public List<TischEintragDto> TischInhalt
        {
            get => _tischInhalt ?? (_tischInhalt = new List<TischEintragDto>());
            set => _tischInhalt = value;
        }
    }
}