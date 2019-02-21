using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace heroes.avalonia.Views
{
    public class MessagesView : UserControl
    {
        public MessagesView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
