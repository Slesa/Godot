using System;
using System.Collections.Generic;
using pos.domain.Tische;

namespace pos.domain
{
    public class TischEventStore
    {
        readonly List<ITischEvent> _events;

        // Serialisierung, lese kompletten Inhalt
        public TischEventStore(TischNr tischnr, List<ITischEvent> eventList)
        {
            TischNr = tischnr;
            _events = eventList;
        }
        public TischEventStore(TischNr tischnr)
        {
            TischNr = tischnr;
            _events = new List<ITischEvent>();
        }

        public TischNr TischNr { get; }
        public IEnumerable<ITischEvent> Events => _events;

        public void AddEvent(ITischEvent tischEvent)
        {
            // Todo: in ctor or here? tischEvent.Id = GetNächsteId();
            _events.Add(tischEvent);
        }
        /* uint GetNächsteId()
        {
            return (uint) _events.Count + 1u;
        } */

        public Tisch Replay()
        {
            var tischInhalt = new List<ITischEvent>();
            foreach(var tischEvent in Events)
                ProcessEvent(tischInhalt, tischEvent);
            var tisch = Tisch.ErzeugeBestehendenTisch(TischNr, tischInhalt);
            return tisch;
        }

        public void ProcessEvent(List<ITischEvent> tischInhalt, ITischEvent tischEvent)
        {
            foreach(var processor in EventProcessors) 
            {
                if( processor.Handled(tischInhalt, tischEvent) ) break;
            }
        }

        IEnumerable<IProcessTischEvent> _eventProcessors;
        IEnumerable<IProcessTischEvent> EventProcessors => _eventProcessors ?? (_eventProcessors = getEventProcessors());

        IEnumerable<IProcessTischEvent> getEventProcessors()
        {
            yield return new ProcessArtikelBestelltEvent();
            yield return new ProcessArtikelStorniertEvent();
        }
    }
}