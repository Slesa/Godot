using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class PersonenListeViewModel : BindableBase
    {
        readonly IRegionManager _regionManager;
        readonly IPersonRepository _personRepository;

        public PersonenListeViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IPersonRepository personRepository)
        {
            _regionManager = regionManager;
            _personRepository = personRepository;
            eventAggregator.GetEvent<ReloadDataEvent>().Subscribe(ReloadPersonen);

            Personen = new ObservableCollection<PersonenViewModel>(_personRepository.GetAll().Select(x => new PersonenViewModel(x)));
            //OnAddCommand = new DelegateCommand(OnAdd);

            PersonSelectedCommand = new DelegateCommand<PersonenViewModel>(PersonSelected);
            //LöschePersonRequest = new InteractionRequest<IConfirmation>();
            //LöschePersonCommand = new DelegateCommand(RaiseConfirmation);
        }

        void ReloadPersonen()
        {
            Personen.Clear();
            Personen.AddRange(_personRepository.GetAll().Select(x => new PersonenViewModel(x)));
        }

        public ObservableCollection<PersonenViewModel> Personen { get; private set; }

        #region Selection
        public DelegateCommand<PersonenViewModel> PersonSelectedCommand { get; private set; }
        void PersonSelected(PersonenViewModel person)
        {
            var parameters = new NavigationParameters {{"person", person}};
            if (person != null)
                _regionManager.RequestNavigate("PersonenEditRegion", "PersonenEditView", parameters);
        }
        #endregion

        //#region Add
        //public ICommand OnAddCommand { get; set; }
        //void OnAdd()
        //{
        //}
        //#endregion Add

        //public InteractionRequest<IConfirmation> LöschePersonRequest { get; set; }
        //public DelegateCommand LöschePersonCommand { get; set; }

        //void RaiseConfirmation()
        //{
        //    LöschePersonRequest.Raise(new Confirmation
        //        {
        //            Title = "Person löschen",
        //            Content = "Confirmation Message"
        //        },
        //        r => Personen.RemoveAt(0));
        //}

    }
}