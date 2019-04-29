using System.Diagnostics;

namespace PersonalPlanung.Core.Model
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Beruf : ValueObject<Beruf>
    {
        public static Beruf Rentner = new Beruf("Rentner");
        public static Beruf Student = new Beruf("Student");
        public static Beruf Kollege = new Beruf("Kollege");
        public static Beruf Dienstleister = new Beruf("Dienstleister");

        public Beruf(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}