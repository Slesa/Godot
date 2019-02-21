using System.Collections.ObjectModel;
using heroes.avalonia.Models;

namespace heroes.avalonia.ViewModels
{
    public class HeroSearchViewModel
    {
        public ObservableCollection<Hero> Heroes { get; set; }
        public string SearchTerm { get; set; }


    }
}