using System;
using System.Collections.ObjectModel;
using PersonalPlanung.Core.Model;
using Prism.Mvvm;

namespace PersonalPlanung.Gui.ViewModels
{
    public class VeranstaltungsViewModel : BindableBase
    {
        public VeranstaltungsViewModel(Veranstaltung veranstaltung)
        {
            Name = veranstaltung.Name;
            BeginntAm = veranstaltung.BeginntAm;
            EndetAm = veranstaltung.EndetAm;
            Aufgaben = new ObservableCollection<Aufgabe>(veranstaltung.Aufgaben);
            AufgabenAnzahl = veranstaltung.Aufgaben.Count;
        }

        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        DateTime _endetAm;
        public DateTime EndetAm
        {
            get => _endetAm;
            set => SetProperty(ref _endetAm, value);
        }

        DateTime _beginntAm;
        public DateTime BeginntAm
        {
            get => _beginntAm;
            set => SetProperty(ref _beginntAm, value);
        }

        public int AufgabenAnzahl { get; set; }
        public ObservableCollection<Aufgabe> Aufgaben { get; set; }
    }
}