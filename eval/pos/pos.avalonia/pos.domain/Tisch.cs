using System.Collections.Generic;
using pos.data;
using EventId = System.UInt64;
using TischNr = System.UInt64;
using ParteiNr = System.UInt32;

namespace pos.domain
{
    public class Währung
    {
        public string Name { get; set; }
        public string Kürzel { get; set; }
    }

    public class GeldBetrag
    {
        public GeldBetrag(decimal betrag, Währung währung)
        {
            Betrag = betrag;
            Währung = währung;
        }
        public decimal Betrag { get; }
        public Währung Währung { get; }
    }

    public class BestelleArtikelCommand 
    {
        public BestelleArtikelCommand(uint anzahl, Artikel artikel)
        {
            Anzahl = anzahl;
            Artikel = artikel;
        }

        public uint Anzahl { get; }
        public Artikel Artikel { get; }
    }

    public class StornierePluCommand
    {
        public StornierePluCommand(uint anzahl, uint plu)
        {
            Anzahl = anzahl;
            Plu = plu;
        }

        public uint Anzahl { get; }
        public uint Plu { get; }
    }




    public class Tisch
    {

        public Tisch(TischNr tischnr, ParteiNr parteinr)
        {
            TischNr = tischnr;
            ParteiNr = parteinr;
            //_events = new List<ITischEvent>();
        }

        public void BestelleArtikel(BestelleArtikelCommand bestelle)
        {
            //_events.Add(new ArtikelBestelltEvent(GetNächsteId(), bestelle.Anzahl, bestelle.Artikel.Plu, bestelle.Artikel.Preis));
        }

        public void StornierePlu(StornierePluCommand storno)
        {
            /*
            var zuStornieren = storno.Anzahl;
            var bestellungen = _events
                .Where(x => x.GetType() == typeof(ArtikelBestelltEvent))
                .Cast<ArtikelBestelltEvent>()
                .Where(x => x.Plu == storno.Plu)
                .OrderByDescending(x => x.Id)
                .ToList();
            foreach (var bestellung in bestellungen)
            {
                var anzahl = bestellung.Anzahl;
                if (anzahl < zuStornieren)
                {
                    _events.Add(new ArtikelStorniertEvent(GetNächsteId(), anzahl, bestellung));
                    zuStornieren -= anzahl;
                    continue;
                }
                _events.Add(new ArtikelStorniertEvent(GetNächsteId(), storno.Anzahl, bestellung));
            }
            */
        }


        public decimal Betrag
        {
            get
            {
                return 0M;
            /* 
                var bestellt = _events
                    .Where(x => x.GetType()==typeof(ArtikelBestelltEvent))
                    .Cast<ArtikelBestelltEvent>()
                    .Sum(x => x.Anzahl * x.Preis);
                var storniert = _events
                    .Where(x => x.GetType() == typeof(ArtikelStorniertEvent))
                    .Cast<ArtikelStorniertEvent>()
                    .Sum(x => x.Betrag);
                return bestellt - storniert;
            */
            }
        }
        public TischNr TischNr { get; }
        public ParteiNr ParteiNr { get; }
        public IEnumerable<ITischEvent> Events => null; //_events;

        uint GetNächsteId()
        {
            return (uint) 1; //_events.Count + 1u;
        }
    }
}
