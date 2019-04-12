using System.Globalization;
using System.Threading;
using System.Windows;
using CommonServiceLocator;
using PersonalPlanung.Core;
using PersonalPlanung.Core.Business;
using PersonalPlanung.Core.Repositories;
using PersonalPlanung.Gui;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;

namespace PersonalPlanung
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IStatusRepository, StatusRepository>();
            containerRegistry.RegisterSingleton<IRolleRepository, RolleRepository>();
            containerRegistry.RegisterSingleton<IPersonRepository, PersonRepository>();
            containerRegistry.RegisterSingleton<IVeranstaltungRepository, VeranstaltungRepository>();
            containerRegistry.RegisterSingleton<ISchichtRepository, SchichtRepository>();
            containerRegistry.RegisterSingleton<IZeitBuchungRepository, ZeitBuchungRepository>();
            containerRegistry.RegisterSingleton<IchFindePersonal, PersonalFinder>();
            containerRegistry.RegisterSingleton<SchichtPlaner, SchichtPlaner>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<GuiModule>();
        }

        protected override Window CreateShell()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }
    }
}
