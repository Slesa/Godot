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
            containerRegistry.RegisterForNavigation<VeranstaltungsListeView>();
            containerRegistry.RegisterForNavigation<VeranstaltungsEditView>();
            containerRegistry.RegisterForNavigation<PersonenListeView>();
            containerRegistry.RegisterForNavigation<PersonenEditView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ShellView));
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonenListeView));

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

            var rolleRepository = containerProvider.Resolve<IRolleRepository>();
            foreach (var rolle in rollen.Distinct())
                rolleRepository.Add(rolle);

            var statusRepository = containerProvider.Resolve<IStatusRepository>();
            foreach(var status in GetPossibleStatus())
                statusRepository.Add(status);

            var veranstaltungRepository = containerProvider.Resolve<IVeranstaltungRepository>();
            var veranstaltungen = importer.ImportiereVeranstaltungen();
            foreach (var veranstaltung in veranstaltungen)
                veranstaltungRepository.Add(veranstaltung);

            var eventAggregator = containerProvider.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ReloadDataEvent>().Publish();
        }

        IEnumerable<Status> GetPossibleStatus()
        {
            yield return Status.Rentner;
            yield return Status.Student;
            yield return Status.Kollege;
            yield return Status.Dienstleister;
        }
    }
}