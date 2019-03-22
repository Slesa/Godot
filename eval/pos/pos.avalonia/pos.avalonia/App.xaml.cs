using Avalonia;
using Avalonia.Markup.Xaml;

namespace pos.avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
