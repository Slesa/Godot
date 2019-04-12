namespace PersonalPlanung.Core.Model
{
    public class Schicht
    {
        public Posten Posten { get; set; }
        public Person Person { get; set; }
        public Veranstaltung Veranstaltung { get; set; }
    }
}