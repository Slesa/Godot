namespace pos.data
{
    /// <summary>
    /// Ein Kellner in der POS Umgebung
    /// </summary>
    public class Kellner : PosDatum
    {
        public string Name { get; set; }
        public string Passwort { get; set; }
    }
}