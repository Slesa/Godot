using pos.data;

namespace pos.domain.Tische
{
    public class BestelleArtikelCommand
    {
        public BestelleArtikelCommand(TischKellner kellner, uint anzahl, Artikel artikel)
        {
            Kellner = kellner;
            Anzahl = anzahl;
            Artikel = artikel;
        }
        public TischKellner Kellner { get; }
        public uint Anzahl { get; }
        public Artikel Artikel { get; }
    }

    public class StornierePluCommand
    {
        public StornierePluCommand(TischKellner kellner, uint anzahl, uint plu)
        {
            Kellner = kellner;
            Anzahl = anzahl;
            Plu = plu;
        }

        public TischKellner Kellner { get; }
        public uint Anzahl { get; }
        public uint Plu { get; }
    }
}
