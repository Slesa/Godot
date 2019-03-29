using ddd.core;

namespace pos.domain.Tische
{
    public class TischKellner : ValueObject<TischKellner>
    {
        public TischKellner(uint id, string name)
        {
            Id = id;
            Name = name;
        }
        public uint Id { get; }
        public string Name { get;  }

        public static TischKellner Null =  new TischKellner(0, "");
    }
}