using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class ShellMenuItem
    {
        public string Name { get; set; }
        public BitmapImage Icon { get; set; }
        public string ViewName { get; set; }
    }

    public class ShellViewModel
    {
        readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MenuItemCommand = new DelegateCommand<ShellMenuItem>(Navigate);
        }

        public DelegateCommand<ShellMenuItem> MenuItemCommand { get; }
        void Navigate(ShellMenuItem menuItem)
        {
            if(menuItem!=null)
                _regionManager.RequestNavigate("ShellContent", menuItem.ViewName);
        }
    }
}