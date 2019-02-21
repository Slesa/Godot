using System.Collections.ObjectModel;
using System.Reactive;
using heroes.avalonia.Models;
using ReactiveUI;

namespace heroes.avalonia.ViewModels
{
    public class HeroesViewModel
    {
        public HeroesViewModel()
        {
            Heroes = new ObservableCollection<Hero>();
            AddCommand = ReactiveCommand.Create(Add);
            DeleteCommand = ReactiveCommand.Create(Delete);
        }

        public ObservableCollection<Hero> Heroes { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; }

        void Add()
        {
        }

        void Delete()
        {
        }
    }
}