using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net62209Practice.App.Wpf.ViewModels;
using Net62209Practice.App.Wpf.Views;
using Net62209Practice.BL.Bootstrapping;
using System.Windows;

namespace Net62209Practice.App.Wpf;

public partial class App
{
    private IHost host; /* TODO: Shlomi, why warning here? */


    private static IHost CreateHost(StartupEventArgs args)
    {
        IHostBuilder hostBuilder = Bootstrapper
            .CreateHostBuilder(args.Args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<MainWindow>();
            });

        return hostBuilder.Build();
    }

    protected override async void OnStartup(StartupEventArgs args)
    {
        host = CreateHost(args);

        await host.StartAsync(); /* TODO: Shlomi, why?? */

        host.Services.GetService<MainWindow>()!.Show();

        base.OnStartup(args);
    }
}
