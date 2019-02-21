using System.Reactive;
using ReactiveUI;

namespace heroes.avalonia.ViewModels
{
    public class HeroDetailViewModel
    {
        public HeroDetailViewModel()
        {
            SaveCommand = ReactiveCommand.Create(Save);
            GoBackCommand = ReactiveCommand.Create(GoBack);
        }

        public string HeroName { get; set; }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        void Save()
        {
        }

        void GoBack()
        {
        }
    }
}