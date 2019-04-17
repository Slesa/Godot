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
        readonly IEventAggregator _eventAggregator;
        readonly IRegionManager _regionManager;
        readonly IPersonRepository _personRepository;

        public PersonenListeViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IPersonRepository personRepository)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _personRepository = personRepository;
            eventAggregator.GetEvent<ReloadDataEvent>().Subscribe(ReloadPersonen);

            Personen = new ObservableCollection<PersonenViewModel>(_personRepository.GetAll().Select(x => new PersonenViewModel(x)));
            //OnAddCommand = new DelegateCommand(OnAdd);

            PersonSelectedCommand = new DelegateCommand<PersonenViewModel>(PersonSelected);
            LöschePersonRequest = new InteractionRequest<IConfirmation>();
            LöschePersonCommand = new DelegateCommand(RaiseConfirmation, CanPersonLöschen).ObservesProperty(() => AktuellePerson);
        }

        void ReloadPersonen()
        {
            Personen.Clear();
            Personen.AddRange(_personRepository.GetAll().Select(x => new PersonenViewModel(x)));
        }

        public ObservableCollection<PersonenViewModel> Personen { get; private set; }

        #region Selection

        PersonenViewModel _aktuellePerson;
        public PersonenViewModel AktuellePerson
        {
            get => _aktuellePerson;
            set => this.SetProperty(ref _aktuellePerson, value);
        }

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

        public InteractionRequest<IConfirmation> LöschePersonRequest { get; private set; }
        public DelegateCommand LöschePersonCommand { get; private set; }

        bool CanPersonLöschen()
        {
            return AktuellePerson != null;
        }

        void RaiseConfirmation()
        {
            LöschePersonRequest.Raise(new Confirmation
            {
                Title = "Person löschen",
                Content = $"Soll die aktuelle Person {AktuellePerson.Name} wirklich aus der Liste entfernt werden?"
            }, OnConfirmationClosed);
        }

        void OnConfirmationClosed(IConfirmation answer)
        {
            if (!answer.Confirmed) return;

            var editView = _regionManager.Regions["PersonenEditRegion"];
            editView.RemoveAll();

            var aktuellePerson = AktuellePerson;
            _eventAggregator.GetEvent<PersonGelöschtEvent>().Publish(aktuellePerson);
            AktuellePerson = null;
            _personRepository.Remove(aktuellePerson.ToPerson());
            var idx = Personen.IndexOf(aktuellePerson);
            Personen.RemoveAt(idx);
            //Personen.Remove(aktuellePerson);
            //var newIdx = idx == 0 ? 1 : idx - 1;
            //AktuellePerson = Personen[newIdx];
        }
    }
}