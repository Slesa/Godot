using PersonalPlanung.Core.Model;
using Prism.Mvvm;

namespace PersonalPlanung.Gui.ViewModels
{
    public class RollenViewModel: BindableBase
    {
        public RollenViewModel(Rolle rolle, bool aktiv)
        {
            Name = rolle.Name;
            Aktiv = aktiv;
        }
        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        bool _aktiv;
        public bool Aktiv
        {
            get => _aktiv;
            set => SetProperty(ref _aktiv, value);
        }
    }
}