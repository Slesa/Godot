using System;
using System.Collections.ObjectModel;
using System.Linq;
using PersonalPlanung.Core.Business;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class SchichtListeViewModel
    {
        readonly IEventAggregator _eventAggregator;
        readonly IRegionManager _regionManager;
        readonly ISchichtRepository _schichtRepository;
        readonly SchichtPlaner _schichtPlaner;

        public SchichtListeViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ISchichtRepository schichtRepository, SchichtPlaner schichtPlaner)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _schichtRepository = schichtRepository;
            _schichtPlaner = schichtPlaner;

            Schichten = new ObservableCollection<Schicht>(schichtRepository.GetAll());
            SchichtSelectedCommand = new DelegateCommand<Schicht>(SchichtSelected);

            PlaneSchichtCommand = new DelegateCommand(OnPlaneSchicht);
        }

        void ReloadSchicht()
        {
            Schichten.Clear();
            Schichten.AddRange(_schichtRepository.GetAll());
        }

        public ObservableCollection<Schicht> Schichten { get; set; }

        #region Schicht planen
        public DelegateCommand PlaneSchichtCommand { get; }
        void OnPlaneSchicht()
        {
            _schichtPlaner.Plane(DateTime.Now);
            ReloadSchicht();
        }
        #endregion

        #region Selection
        public DelegateCommand<Schicht> SchichtSelectedCommand { get; }
        void SchichtSelected(Schicht schicht)
        {
            var parameters = new NavigationParameters { { "schicht", schicht } };
            if (schicht != null)
                _regionManager.RequestNavigate("SchichtEditRegion", "SchichtEditView", parameters);
        }
        #endregion


    }
}