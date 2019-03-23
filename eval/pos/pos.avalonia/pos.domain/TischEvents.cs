using System;
using ddd.core.Events;

namespace pos.domain
{
    public interface ITischEvent : IDomainEvent
    {
        uint Id { get; }
    }

    public class TischEvent : ITischEvent
    {
        public TischEvent(uint id)
        {
            Id = id;
            OccurredOn = DateTime.Now;
        }
        public DateTime OccurredOn { get; }
        public uint Id { get; }
    }

    public class ArtikelBestelltEvent: TischEvent
    {
        public ArtikelBestelltEvent(uint id, uint anzahl, uint plu, decimal preis)
        : base(id)
        {
            Anzahl = anzahl;
            Plu = plu;
            Preis = preis;
        }

        public uint Anzahl { get; }
        public uint Plu { get; }
        public decimal Preis { get; set; }
    }

    public class ArtikelStorniertEvent : TischEvent
    {
        public ArtikelStorniertEvent(uint id, uint anzahl, ArtikelBestelltEvent bestellung)
        : base(id)
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
