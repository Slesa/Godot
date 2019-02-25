using System;
using System.Collections.Generic;
using System.Text;
using heroes.avalonia.Models;

namespace heroes.avalonia.ViewModels
{
    // ReactiveUI List: https://janhannemann.wordpress.com/2016/10/06/reactiveui-goodies-reactivelist/
    // ReactiveUI Properties: https://janhannemann.wordpress.com/2016/10/03/reactiveui-goodies-observing-properties/
    public class MainWindowViewModel : ViewModelBase
    {
        Startup _startup;
        public MainWindowViewModel() 
        {
            _startup = new Startup();
        }

        HeroDetailViewModel _heroDetailViewModel;
        public HeroDetailViewModel HeroDetailViewModel {
            get { return (HeroDetailViewModel)_startup.ServiceProvider.GetService(typeof(HeroDetailViewModel)); }
/*                if( _heroDetailViewModel==null) 
                    _heroDetailViewModel = new HeroDetailViewModel();
                return _heroDetailViewModel;
            } */
        }

        HeroesViewModel _heroesViewModel;
        public HeroesViewModel HeroesViewModel {
            get { return (HeroesViewModel)_startup.ServiceProvider.GetService(typeof(HeroesViewModel)); }
/*                if( _heroesViewModel==null) 
                    _heroesViewModel = new HeroesViewModel();
                return _heroesViewModel;
            } */
        }
    }
}
