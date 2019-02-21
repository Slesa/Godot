using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using heroes.avalonia.ViewModels;
using heroes.avalonia.Views;

namespace heroes.avalonia
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
