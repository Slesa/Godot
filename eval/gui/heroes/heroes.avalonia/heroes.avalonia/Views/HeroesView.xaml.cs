using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace heroes.avalonia.Views
{
    public class HeroesView : UserControl
    {
        public HeroesView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
