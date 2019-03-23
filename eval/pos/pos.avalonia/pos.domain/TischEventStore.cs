using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace pos.domain
{
    [XmlRoot("Tisch")]
    [XmlInclude(typeof(ArtikelBestelltEvent))]
    [XmlInclude(typeof(ArtikelStorniertEvent))]
    public class TischEventStore
    {
        List<ITischEvent> _events;

        // XML-Serializer braucht parameterlosen ctor
        public TischEventStore()
        {
            _events = new List<ITischEvent>();
        }
        public TischEventStore(uint tischnr, uint parteinr)
        : base()
        {
            TischNr = tischnr;
            ParteiNr = parteinr;
            _events = new List<ITischEvent>();
        }
        [System.Xml.Serialization.XmlIgnore]
        public uint TischNr { get; }
        [System.Xml.Serialization.XmlIgnore]
        public uint ParteiNr { get; }
        [System.Xml.Serialization.XmlIgnore]
        public IEnumerable<ITischEvent> Events => _events;

        // XML-Serializer braucht eine Liste
        [XmlArray("TischInhalt")]
        [XmlArrayItem("TischEintrag")]
        public List<ITischEvent> Tischinhalt {
            get { return _events.ToList(); }
            set { _events = value; }
        }
        uint GetNächsteId()
        {
            return (uint) _events.Count + 1u;
        }
    }
}