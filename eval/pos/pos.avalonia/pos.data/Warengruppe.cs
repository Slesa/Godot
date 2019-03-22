namespace pos.data
{
    /// <summary>
    /// Eine Warengruppe in der POS Umgebung. Hierunter werden Artikel einsortiert.
    /// </summary>
    public class Warengruppe : PosDatum
    {
        public string Name { get; set; }
    }
}