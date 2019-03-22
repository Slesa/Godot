using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using pos.avalonia.ViewModels;
using pos.avalonia.Views;

namespace pos.avalonia
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
