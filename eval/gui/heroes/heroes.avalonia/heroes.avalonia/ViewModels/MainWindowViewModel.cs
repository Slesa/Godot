using System;
using System.Collections.Generic;
using System.Text;

namespace heroes.avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Hello World!";

        HeroDetailViewModel _heroDetailViewModel;
        public HeroDetailViewModel HeroDetailViewModel {
            get { 
                if( _heroDetailViewModel==null) 
                    _heroDetailViewModel = new HeroDetailViewModel();
                return _heroDetailViewModel;
            }
        }
    }
}
