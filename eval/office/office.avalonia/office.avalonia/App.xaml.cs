using Avalonia;
using Avalonia.Markup.Xaml;

namespace office.avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
