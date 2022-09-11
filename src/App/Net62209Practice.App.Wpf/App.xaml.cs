using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net62209Practice.BL.Bootstrapping;
using System.Windows;
using Net62209Practice.App.Wpf.ViewModels;

namespace Net62209Practice.App.Wpf;

public partial class App
{
    private IHost host;


    protected override async void OnStartup(StartupEventArgs args)
    {
        host = CreateHost(args);

        await host.StartAsync(); /* TODO: Shlomi, why?? */

        var mainWindow = host.Services.GetRequiredService<MainWindow>(); /* TODO: Shlomi, use the UI framework to hot-wire the View and the VM */
        mainWindow.DataContext = host.Services.GetRequiredService<MainWindowViewModel>();

        mainWindow.Show();

        base.OnStartup(args);
    }

    private static IHost CreateHost(StartupEventArgs args)
    {
        IHostBuilder hostBuilder = Bootstrapper.Register(args.Args);
        hostBuilder.ConfigureServices((_, services) => services.AddSingleton<MainWindowViewModel>());
        hostBuilder.ConfigureServices((_, services) => services.AddSingleton<MainWindow>()); /* TODO: Shlomi, how to register ViewModel -> View? */

        return hostBuilder.Build();
    }
}
