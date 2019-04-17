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
            Beruf = person.Beruf;
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

        Beruf _beruf;
        public Beruf Beruf
        {
            get => _beruf;
            set => SetProperty(ref _beruf, value);
        }

        public List<Rolle> EinsetzbarAls { get; set; }

        public Person ToPerson()
        {
            return new Person {Name = Name, Vorname = Vorname, Beruf = Beruf, EinsetzbarAls = EinsetzbarAls.ToList()};
        }
    }
}