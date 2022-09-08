using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net62209Practice.BL.Bootstrapping;
using System.Windows;

namespace Net62209Practice.App.Wpf;

public partial class App
{
    private IHost host;


    protected override async void OnStartup(StartupEventArgs args)
    {
        host = CreateHost(args);

        await host.StartAsync(); /* TODO: Shlomi, why?? */

        var mainWindow = host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(args);
    }

    private static IHost CreateHost(StartupEventArgs args)
    {
        IHostBuilder hostBuilder = Bootstrapper.Register(args.Args);
        hostBuilder.ConfigureServices((_, services) => services.AddSingleton<MainWindow>());

        return hostBuilder.Build();
    }
}
