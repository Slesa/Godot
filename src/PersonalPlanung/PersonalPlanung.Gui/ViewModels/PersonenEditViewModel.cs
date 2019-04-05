using System.Collections.Generic;
using System.Linq;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;
using Prism.Mvvm;
using Prism.Regions;

namespace PersonalPlanung.Gui.ViewModels
{
    public class PersonenEditViewModel : BindableBase, INavigationAware
    {
        readonly IPersonRepository _personRepository;
        readonly IRolleRepository _rolleRepository;

        public PersonenEditViewModel(IPersonRepository personRepository, IRolleRepository rolleRepository)
        {
            _personRepository = personRepository;
            _rolleRepository = rolleRepository;
            EinsetzbarAls = new List<RollenViewModel>(rolleRepository.GetAll().Select(x => new RollenViewModel(x, false)));
        }

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

        Status _status;
        public Status Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        List<RollenViewModel> _einsetzbarAls;
        public List<RollenViewModel> EinsetzbarAls
        {
            get => _einsetzbarAls;
            set => SetProperty(ref _einsetzbarAls, value);
        }

        bool IsNew { get; set; }
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
            Status = AktuellePerson.Status;
            EinsetzbarAls = new List<RollenViewModel>(_rolleRepository.GetAll().Select(x => new RollenViewModel(x, AktuellePerson.EinsetzbarAls.Contains(x))));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            AktuellePerson.Name = Name;
            AktuellePerson.Vorname = Vorname;
            AktuellePerson.Status = AktuellePerson.Status;
            AktuellePerson.EinsetzbarAls = new List<Rolle>(EinsetzbarAls.Where(x => x.Aktiv).Select(r => new Rolle(r.Name)));
            if(IsNew)
                _personRepository.Add(AktuellePerson.ToPerson());
        }
    }

}