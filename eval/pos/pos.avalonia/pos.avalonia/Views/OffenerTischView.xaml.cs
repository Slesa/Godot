using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace pos.avalonia.Views
{
    public class OffenerTischView : UserControl
    {
        public OffenerTischView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
