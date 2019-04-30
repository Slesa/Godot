using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using PersonalPlanung.Core.Repositories;
using Prism.Events;

namespace PersonalPlanung.Gui.ViewModels
{
    public class ZeitBuchungsListeViewModel
    {
        readonly Dispatcher _dispatcher;
        readonly IZeitBuchungRepository _zeitbuchungRepository;

        public ZeitBuchungsListeViewModel(IEventAggregator eventAggregator, IZeitBuchungRepository zeitbuchungRepository)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _zeitbuchungRepository = zeitbuchungRepository;
            eventAggregator.GetEvent<ReloadDataEvent>().Subscribe(ReloadZeitBuchungen);

            ZeitBuchungen = new ObservableCollection<ZeitBuchungsViewModel>(zeitbuchungRepository.GetAll().Select(x => new ZeitBuchungsViewModel(x)));

        }

        void ReloadZeitBuchungen()
        {
            _dispatcher.Invoke(() =>
            {
                ZeitBuchungen.Clear();
                ZeitBuchungen.AddRange(_zeitbuchungRepository.GetAll().Select(x => new ZeitBuchungsViewModel(x)));
            });
        }

        public ObservableCollection<ZeitBuchungsViewModel> ZeitBuchungen { get; set; }

    }
}