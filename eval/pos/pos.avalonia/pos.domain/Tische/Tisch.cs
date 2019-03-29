using System.Collections.Generic;
using System.Linq;
using pos.data;

namespace pos.domain.Tische
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



    public class Tisch
    {
        internal Tisch(TischNr tischnr, List<ITischEvent> tischInhalt)
        {
            TischNr = tischnr;
            _events = tischInhalt;
        }

        public static Tisch ErzeugeBestehendenTisch(TischNr tischnr, List<ITischEvent> tischInhalt)
        {
            return new Tisch(tischnr, tischInhalt);
        }

        public Tisch(TischNr tischnr)
        {
            TischNr = tischnr;
            _events = new List<ITischEvent>();
        }

        public void BestelleArtikel(BestelleArtikelCommand bestelle)
        {
            if(!_events.Any())
                _events.Add(new TischErzeugtEvent(bestelle.Kellner));
            AktuelleRunde+=1;
            _events.Add(new ArtikelBestelltEvent(bestelle.Kellner, bestelle.Anzahl, bestelle.Artikel.Plu, bestelle.Artikel.Preis));
        }

        public void StornierePlu(StornierePluCommand storno)
        {
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
                    bestellung.Anzahl = 0;
                    _events.Add(new ArtikelStorniertEvent(storno.Kellner, anzahl, bestellung));
                    zuStornieren -= anzahl;
                    continue;
                }
                bestellung.Anzahl -= zuStornieren;
                _events.Add(new ArtikelStorniertEvent(storno.Kellner, zuStornieren, bestellung));
                break;
            }
        }


        public decimal Betrag
        {
            get
            {
                var bestellt = _events
                    .Where(x => x.GetType()==typeof(ArtikelBestelltEvent))
                    .Cast<ArtikelBestelltEvent>()
                    .Sum(x => x.Anzahl * x.Preis);
                return bestellt;
                /* var storniert = _events
                    .Where(x => x.GetType() == typeof(ArtikelStorniertEvent))
                    .Cast<ArtikelStorniertEvent>()
                    .Sum(x => x.Betrag);
                return bestellt - storniert; */
            }
        }

        public TischNr TischNr { get; }
        public uint AktuelleRunde { get; private set; }
        readonly List<ITischEvent> _events;
        public IEnumerable<ITischEvent> Events => _events;
    }
}
