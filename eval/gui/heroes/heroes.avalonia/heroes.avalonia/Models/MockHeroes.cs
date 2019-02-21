using System.Collections.Generic;

namespace heroes.avalonia.Models
{
    public class MockHeroes
    {
        public IEnumerable<Hero> Heroes
        {
            get
            {
                yield return new Hero {Id = 11, Name = "Mr. Nice"};
                yield return new Hero {Id = 12, Name = "Narco"};
                yield return new Hero { Id = 13, Name = "Bombasto" };
                yield return new Hero { Id = 14, Name = "Celeritas" };
                yield return new Hero { Id = 15, Name = "Magneta" };
                yield return new Hero { Id = 16, Name = "RubberMan" };
                yield return new Hero { Id = 17, Name = "Dynama" };
                yield return new Hero { Id = 18, Name = "Dr IQ" };
                yield return new Hero { Id = 19, Name = "Magma" };
                yield return new Hero { Id = 20, Name = "Tornado" };
            }
        }
    }
}