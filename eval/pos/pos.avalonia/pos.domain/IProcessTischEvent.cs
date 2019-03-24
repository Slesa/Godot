using System.Collections.Generic;
using System.Linq;

namespace pos.domain
{
    public interface IProcessTischEvent
    {
         bool Handled(List<ITischEvent> tischInhalt, ITischEvent tischEvent);
    }

    public class ProcessArtikelBestelltEvent : IProcessTischEvent
    {
         public bool Handled(List<ITischEvent> tischInhalt, ITischEvent tischEvent)
         {
             var bestellung = tischEvent as ArtikelBestelltEvent;
             if(bestellung==null) return false;
            tischInhalt.Add(bestellung);
            return true;
         }
    }

    public class ProcessArtikelStorniertEvent : IProcessTischEvent
    {
         public bool Handled(List<ITischEvent> tischInhalt, ITischEvent tischEvent)
         {
             var storno = tischEvent as ArtikelStorniertEvent;
             if(storno==null) return false;
             var bestellung = (ArtikelBestelltEvent) tischInhalt.FirstOrDefault(x => x.Id==storno.Bestellung);
             if(bestellung!=null)
                bestellung.Anzahl -= storno.Anzahl;
            return true;
         }

    }
}