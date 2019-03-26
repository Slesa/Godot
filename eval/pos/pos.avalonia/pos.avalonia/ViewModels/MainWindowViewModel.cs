using System;
using System.Collections.Generic;
using System.Text;

namespace pos.avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            OffenerTischViewModel = new OffenerTischViewModel();
        }
        public OffenerTischViewModel OffenerTischViewModel { get; set; }
    }
}
