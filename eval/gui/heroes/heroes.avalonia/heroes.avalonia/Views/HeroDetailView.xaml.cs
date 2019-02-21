using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace heroes.avalonia.Views
{
    public class HeroDetailView : UserControl
    {
        public HeroDetailView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
