using PersonalPlanung.Gui.ViewModels;
using Prism.Events;

namespace PersonalPlanung.Gui
{
    public class ReloadDataEvent : PubSubEvent {}

    public class PersonGelöschtEvent : PubSubEvent<PersonenViewModel> {}

}