namespace pos.data
{
    /// <summary>
    /// Ein Artikel in der POS Umgebung
    /// </summary>
    public class Artikel : PosDatum
    {
        public uint Plu { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
        public uint Warengruppe { get; set; }
    }
}
