using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class PersonenEditViewModel : BindableBase, INavigationAware
    {
        readonly IPersonRepository _personRepository;
        readonly IRolleRepository _rolleRepository;

        public PersonenEditViewModel(IEventAggregator eventAggregator, IPersonRepository personRepository, IRolleRepository rolleRepository, IBerufRepository berufRepository)
        {
            eventAggregator.GetEvent<PersonGelöschtEvent>().Subscribe(_ =>
            {
                AktuellePerson = null;
                DiscardChanges = true;
            });
            _personRepository = personRepository;
            _rolleRepository = rolleRepository;
            EinsetzbarAls = new List<RollenViewModel>(rolleRepository.GetAll().Select(x => new RollenViewModel(x, false)));

            BerufListe = new ObservableCollection<Beruf>(new List<Beruf> {new Beruf("")});
            BerufListe.AddRange(berufRepository.GetAll());
        }

        public ObservableCollection<Beruf> BerufListe { get; }

        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        string _vorname;
        public string Vorname
        {
            get => _vorname;
            set => SetProperty(ref _vorname, value);
        }

        Beruf _beruf;
        public Beruf Beruf
        {
            get => _beruf;
            set => SetProperty(ref _beruf, value);
        }

        List<RollenViewModel> _einsetzbarAls;
        public List<RollenViewModel> EinsetzbarAls
        {
            get => _einsetzbarAls;
            set => SetProperty(ref _einsetzbarAls, value);
        }

        bool IsNew { get; set; }
        bool DiscardChanges { get; set; }
        PersonenViewModel AktuellePerson { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            AktuellePerson = navigationContext.Parameters["person"] as PersonenViewModel;
            if (AktuellePerson == null)
            {
                AktuellePerson = new PersonenViewModel();
                IsNew = true;
            }
            else
            {
                IsNew = false;
            }
            Name = AktuellePerson.Name;
            Vorname = AktuellePerson.Vorname;
            Beruf = AktuellePerson.Beruf;
            EinsetzbarAls = new List<RollenViewModel>(_rolleRepository.GetAll().Select(x => new RollenViewModel(x, AktuellePerson.EinsetzbarAls.Contains(x))));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (DiscardChanges) return;
            if (string.IsNullOrWhiteSpace(Name)) return;
            if (AktuellePerson == null)
            {
                IsNew = true;
                AktuellePerson = new PersonenViewModel();
            }
            AktuellePerson.Name = Name;
            AktuellePerson.Vorname = Vorname;
            AktuellePerson.Beruf = Beruf;
            AktuellePerson.EinsetzbarAls = new List<Rolle>(EinsetzbarAls.Where(x => x.Aktiv).Select(r => new Rolle(r.Name)));
            if(IsNew)
                _personRepository.Add(AktuellePerson.ToPerson());
            else
                _personRepository.Change(AktuellePerson.ToPerson());
        }
    }

}