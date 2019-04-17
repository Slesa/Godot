namespace PersonalPlanung.Core.Model
{
    public class Schicht
    {
        public Aufgabe Aufgabe { get; set; }
        public Person Person { get; set; }
        public Veranstaltung Veranstaltung { get; set; }
    }
}