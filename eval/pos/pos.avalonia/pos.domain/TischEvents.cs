using System;
using ddd.core.Events;

namespace pos.domain
{
    public interface ITischEvent : IDomainEvent
    {
        uint Id { get; set; }
    }

    public class TischEvent : ITischEvent
    {
        public TischEvent()
        {
            OccurredOn = DateTime.Now;
        }
        public DateTime OccurredOn { get; set; }
        public uint Id { get; set; }
    }

    public class ArtikelBestelltEvent: TischEvent
    {
        public ArtikelBestelltEvent(uint anzahl, uint plu, decimal preis)
        {
            Anzahl = anzahl;
            Plu = plu;
            Preis = preis;
        }

        public uint Anzahl { get; set; }
        public uint Plu { get; }
        public decimal Preis { get; set; }
    }

    public class ArtikelStorniertEvent : TischEvent
    {
        public ArtikelStorniertEvent(uint anzahl, uint bestellId, decimal betrag)
        {
            Anzahl = anzahl;
            Bestellung = bestellId;
            Betrag = betrag;
        }
        public ArtikelStorniertEvent(uint anzahl, ArtikelBestelltEvent bestellung)
        {
            Anzahl = anzahl;
            Bestellung = bestellung.Id;
            Betrag = anzahl * bestellung.Preis;
        }

        public uint Anzahl { get; }
        public uint Bestellung { get; }
        public decimal Betrag { get; }
    }
}
