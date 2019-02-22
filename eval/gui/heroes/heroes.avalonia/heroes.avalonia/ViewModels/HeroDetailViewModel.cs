using System.Reactive;
using ReactiveUI;

namespace heroes.avalonia.ViewModels
{
    public class HeroDetailViewModel : ReactiveObject
    {
        public HeroDetailViewModel()
        {
            SaveCommand = ReactiveCommand.Create(Save);
            GoBackCommand = ReactiveCommand.Create(GoBack);
        }

        string _heroName;
        public string HeroName 
        { 
            get { return _heroName; } 
            set { this.RaiseAndSetIfChanged(ref _heroName, value); }
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        void Save()
        {
            System.Console.WriteLine("SAVE");
        }

        void GoBack()
        {
        }
    }
}