using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;
using PersonalPlanung.Excel;
using PersonalPlanung.Gui.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PersonalPlanung.Gui
{
    public class GuiModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SchichtListeView>();
            containerRegistry.RegisterForNavigation<VeranstaltungsListeView>();
            containerRegistry.RegisterForNavigation<VeranstaltungsEditView>();
            containerRegistry.RegisterForNavigation<ZeitBuchungsListeView>();
            containerRegistry.RegisterForNavigation<PersonenListeView>();
            containerRegistry.RegisterForNavigation<PersonenEditView>();
            containerRegistry.RegisterForNavigation<ImportView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ShellView));
            //regionManager.RegisterViewWithRegion("PersonenEditRegion", typeof(PersonenEditView));
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonenListeView));
            /*
            var rolleRepository = containerProvider.Resolve<IRolleRepository>();
            if (rolleRepository.GetAll().Any()) return;


            const string ExcelFile = @"d:\work\github\Godot\src\PersonalPlanung\April.xlsx";
            var importer = new ExcelImporter(ExcelFile);
            var rollen = new List<Rolle>();

            var personRepository = containerProvider.Resolve<IPersonRepository>();
            var personen = importer.ImportierePersonen().Distinct();
            foreach (var person in personen)
            {
                rollen.AddRange(person.EinsetzbarAls);
                personRepository.Add(person);
            }

            foreach (var rolle in rollen.Distinct())
                rolleRepository.Add(rolle);

            var berufRepository = containerProvider.Resolve<IBerufRepository>();
            foreach(var beruf in GetPossibleBeruf())
                berufRepository.Add(beruf);

            var veranstaltungRepository = containerProvider.Resolve<IVeranstaltungRepository>();
            var veranstaltungen = importer.ImportiereVeranstaltungen().ToList();
            foreach (var veranstaltung in veranstaltungen)
                veranstaltungRepository.Add(veranstaltung);

            var schichtRepository = containerProvider.Resolve<ISchichtRepository>();
            foreach (var veranstaltung in veranstaltungen)
            {
                foreach (var aufgabe in veranstaltung.Aufgaben)
                {
                    var schicht = new Schicht {Aufgabe = aufgabe, Veranstaltung = veranstaltung};
                    schichtRepository.Add(schicht);
                }
            }

            var eventAggregator = containerProvider.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ReloadDataEvent>().Publish();
            */
        }
        /*
        IEnumerable<Beruf> GetPossibleBeruf()
        {
            yield return Beruf.Rentner;
            yield return Beruf.Student;
            yield return Beruf.Kollege;
            yield return Beruf.Dienstleister;
        }
        */
    }
}