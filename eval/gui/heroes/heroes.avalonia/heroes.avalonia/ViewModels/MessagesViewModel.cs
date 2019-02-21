using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

namespace heroes.avalonia.ViewModels
{
    public class MessagesViewModel
    {
        public MessagesViewModel()
        {
            Messages = new ObservableCollection<string>();
            ClearCommand = ReactiveCommand.Create(Clear);
        }

        public ObservableCollection<string> Messages { get; }
        public ReactiveCommand<Unit, Unit> ClearCommand { get; }

        void Clear()
        {
        }
    }
}