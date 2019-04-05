using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PersonalPlanung.Core.Model;
using Prism.Mvvm;

namespace PersonalPlanung.Gui.ViewModels
{
    public class PersonenViewModel : BindableBase
    {
        public PersonenViewModel(Person person)
        {
            Name = person.Name;
            Vorname = person.Vorname;
            Status = person.Status;
            EinsetzbarAls = new List<Rolle>(person.EinsetzbarAls);
        }

        public PersonenViewModel()
        {
            EinsetzbarAls = new List<Rolle>();
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

        public List<Rolle> EinsetzbarAls { get; set; }

        public Person ToPerson()
        {
            return new Person {Name = Name, Vorname = Vorname, Status = Status, EinsetzbarAls = EinsetzbarAls.ToList()};
        }
    }
}