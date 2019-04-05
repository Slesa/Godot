using PersonalPlanung.Core.Repositories;
using Prism.Mvvm;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class VeranstaltungsEditViewModel : BindableBase, INavigationAware
    {
        readonly IVeranstaltungRepository _veranstaltungRepository;
        VeranstaltungsViewModel _aktuelleVeranstaltung;

        public VeranstaltungsEditViewModel(IVeranstaltungRepository veranstaltungRepository)
        {
            _veranstaltungRepository = veranstaltungRepository;
        }

        public VeranstaltungsViewModel AktuelleVeranstaltung
        {
            get => _aktuelleVeranstaltung;
            set => SetProperty(ref _aktuelleVeranstaltung, value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            AktuelleVeranstaltung = navigationContext.Parameters["veranstaltung"] as VeranstaltungsViewModel;
            /*if (AktuelleVeranstaltung == null)
            {
                AktuelleVeranstaltung = new VeranstaltungsViewModel();
                IsNew = true;
            }
            else
            {
                IsNew = false;
            }*/
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}