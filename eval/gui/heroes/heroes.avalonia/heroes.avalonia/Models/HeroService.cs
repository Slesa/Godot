using System;
using System.Collections.Generic;

namespace heroes.avalonia.Models
{
    public interface IHeroService
    {
        IEnumerable<Hero> GetHeroes();
        Hero GetHero(int id);
        Hero AddHero(Hero hero);
        //void DeleteHero(int id);
        void DeleteHero(Hero hero);
    }

    public class HeroService : IHeroService
    {
        public IEnumerable<Hero> GetHeroes() 
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

        public Hero GetHero(int id) 
        {
            System.Console.WriteLine("Get hero {1}", id);
            return null;
        }

        public Hero AddHero(Hero hero)
        {
            System.Console.WriteLine("Add hero {1}", hero.Id);
            return hero;
        }
 
        public void DeleteHero(Hero hero)
        {
            System.Console.WriteLine("Delete hero {1}", hero.Id);
        }
    }
}