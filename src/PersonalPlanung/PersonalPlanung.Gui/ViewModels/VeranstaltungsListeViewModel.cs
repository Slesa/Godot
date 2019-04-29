using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using PersonalPlanung.Core.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class VeranstaltungsListeViewModel
    {
        readonly IEventAggregator _eventAggregator;
        readonly IRegionManager _regionManager;
        readonly IVeranstaltungRepository _veranstaltungRepository;
        readonly Dispatcher _dispatcher;

        public VeranstaltungsListeViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IVeranstaltungRepository veranstaltungRepository)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _veranstaltungRepository = veranstaltungRepository;
            eventAggregator.GetEvent<ReloadDataEvent>().Subscribe(ReloadVeranstaltungen);

            Veranstaltungen = new ObservableCollection<VeranstaltungsViewModel>(veranstaltungRepository.GetAll().Select(x => new VeranstaltungsViewModel(x)));

            VeranstaltungSelectedCommand = new DelegateCommand<VeranstaltungsViewModel>(VeranstaltungSelected);
        }

        void ReloadVeranstaltungen()
        {
            _dispatcher.Invoke(() =>
            {
                Veranstaltungen.Clear();
                Veranstaltungen.AddRange(_veranstaltungRepository.GetAll().Select(x => new VeranstaltungsViewModel(x)));
            });
        }

        public ObservableCollection<VeranstaltungsViewModel> Veranstaltungen { get; set; }

        #region Selection
        public DelegateCommand<VeranstaltungsViewModel> VeranstaltungSelectedCommand { get; private set; }
        void VeranstaltungSelected(VeranstaltungsViewModel veranstaltung)
        {
            var parameters = new NavigationParameters { { "veranstaltung", veranstaltung } };
            if (veranstaltung != null)
                _regionManager.RequestNavigate("VeranstaltungsEditRegion", "VeranstaltungsEditView", parameters);
        }
        #endregion

    }
}