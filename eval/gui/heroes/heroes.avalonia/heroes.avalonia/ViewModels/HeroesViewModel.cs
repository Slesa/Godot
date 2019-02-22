using System.Collections.ObjectModel;
using System.Reactive;
using heroes.avalonia.Models;
using ReactiveUI;

namespace heroes.avalonia.ViewModels
{
    public class HeroesViewModel : ReactiveObject
    {
        public HeroesViewModel()
        {
            Heroes = new ObservableCollection<Hero>();
            AddCommand = ReactiveCommand.Create(Add);
            DeleteCommand = ReactiveCommand.Create(Delete);
        }

        string _newHeroName;
        public string NewHeroName 
        { 
            get { return _newHeroName; } 
            set { this.RaiseAndSetIfChanged(ref _newHeroName, value); }
        }

        public ObservableCollection<Hero> Heroes { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; }

        void Add()
        {
            System.Console.WriteLine("ADD");
        }

        void Delete()
        {
            System.Console.WriteLine("DEL");
        }
    }
}