using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using heroes.avalonia.Models;
using ReactiveUI;

namespace heroes.avalonia.ViewModels
{
    public class HeroesViewModel : ReactiveObject
    {
        IHeroService _heroService;

        public HeroesViewModel(IHeroService heroService)
        {
            System.Console.WriteLine("HeroesViewModel");

            _heroService = heroService;

            this.WhenAnyValue(x => x.NewHeroName).Subscribe(name => System.Diagnostics.Debug.WriteLine("$New name is {name}"));
            //Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(15)).Subscribe(_ => Heroes = _heroService.GetHeroes().ToList());

            AddCommand = ReactiveCommand.Create(Add);
            DeleteCommand = ReactiveCommand.Create(Delete);
        }

        string _newHeroName;
        public string NewHeroName 
        { 
            get { return _newHeroName; } 
            set { 
                System.Diagnostics.Debug.WriteLine("Updated hero name {0}", value);
                this.RaisePropertyChanged();
                this.RaiseAndSetIfChanged(ref _newHeroName, value); }
        }

        ObservableCollection<Hero> _heroes;
        public ObservableCollection<Hero> Heroes 
        { 
            get { 
                if( _heroes==null ) {
                    _heroes = new ObservableCollection<Hero>(_heroService.GetHeroes());
                }
                return _heroes;
            } 
            set {  
                System.Diagnostics.Debug.WriteLine("Updated heroes {0}", value.Count);
                this.RaiseAndSetIfChanged(ref _heroes, value); }
        }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; }

        void Add()
        {
            System.Diagnostics.Debug.WriteLine("ADD");
            var name = NewHeroName.Trim();
            if( string.IsNullOrEmpty(name)) return;

            //_heroService.AddHero(name);
        }

        void Delete()
        {
            System.Diagnostics.Debug.WriteLine("DEL");
        }
    }
}