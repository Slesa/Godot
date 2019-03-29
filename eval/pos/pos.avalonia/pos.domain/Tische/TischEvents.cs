using System;
using System.Collections.Generic;
using ddd.core.Events;

namespace pos.domain.Tische
{
    public interface ITischEvent : IDomainEvent
    {
        Guid Id { get; }
        TischKellner Kellner { get; }
    }

    public class TischEvent : ITischEvent
    {
        public TischEvent(TischKellner kellner)
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
            Kellner = kellner;
        }

        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public TischKellner Kellner { get; set; }
    }

    public class TischErzeugtEvent : TischEvent
    {
        public TischErzeugtEvent(TischKellner kellner)
            : base(kellner)
        {
        }
    }

    public class ArtikelBestelltEvent: TischEvent
    {
        public ArtikelBestelltEvent(TischKellner kellner, uint anzahl, uint plu, decimal preis)
            : base(kellner)
        {
            Anzahl = anzahl;
            Plu = plu;
            Preis = preis;
        }

        public uint Anzahl { get; set; }
        public uint Plu { get; set; }
        public decimal Preis { get; set; }
    }

    public class ArtikelStorniertEvent : TischEvent
    {
        public ArtikelStorniertEvent(TischKellner kellner, uint anzahl, Guid bestellId, decimal betrag)
            : base(kellner)
        {
            Anzahl = anzahl;
            Bestellung = bestellId;
            Betrag = betrag;
        }
        public ArtikelStorniertEvent(TischKellner kellner, uint anzahl, ArtikelBestelltEvent bestellung)
            : base(kellner)
        {
            Anzahl = anzahl;
            Bestellung = bestellung.Id;
            Betrag = anzahl * bestellung.Preis;
        }

        public uint Anzahl { get; }
        public Guid Bestellung { get; }
        public decimal Betrag { get; }
    }

    public static class TischEventExtensions
    {
        public static IEnumerable<T> FilterEvents<T>(this IEnumerable<ITischEvent> tischInhalt) where T: ITischEvent
        {
            foreach (var element in tischInhalt)
            {
                if (element.GetType() == typeof(T)) yield return (T) element;
            }
        }
    }
}
