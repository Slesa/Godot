using System.Collections.Generic;

namespace pos.domain
{
    public class TischEventStore
    {
        readonly List<ITischEvent> _events;

        // Serialisierung, lese kompletten Inhalt
        public TischEventStore(uint tischnr, uint parteinr, List<ITischEvent> eventList)
        {
            TischNr = tischnr;
            ParteiNr = parteinr;
            _events = eventList;
        }
        public TischEventStore(uint tischnr, uint parteinr)
        {
            TischNr = tischnr;
            ParteiNr = parteinr;
            _events = new List<ITischEvent>();
        }

        public uint TischNr { get; }
        public uint ParteiNr { get; }
        public IEnumerable<ITischEvent> Events => _events;

        public void AddEvent(ITischEvent tischEvent)
        {
            tischEvent.Id = GetNächsteId();
            _events.Add(tischEvent);
        }
        uint GetNächsteId()
        {
            return (uint) _events.Count + 1u;
        }
    }
}