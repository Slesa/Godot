using Microsoft.Extensions.DependencyInjection;
using heroes.avalonia.ViewModels;

namespace heroes.avalonia.Models
{
    // https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
    // StructureMap?
    public class Startup
    {
        public Startup()
        {
            CreateServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }

        void CreateServiceProvider() 
        {
            ServiceProvider = new ServiceCollection()
                .AddSingleton<IHeroService,HeroService>()
                .AddSingleton<HeroesViewModel,HeroesViewModel>()
                .AddSingleton<HeroDetailViewModel,HeroDetailViewModel>()
                .BuildServiceProvider();
        }
    }
}