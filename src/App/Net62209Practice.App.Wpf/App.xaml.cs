using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net62209Practice.App.Wpf.ViewModels;
using Net62209Practice.BL.Bootstrapping;
using NoNameCompany.Extensions.DependencyInjection;
using System.Windows;
using Net62209Practice.App.Wpf.Views;

namespace Net62209Practice.App.Wpf;

public partial class App
{
    private IHost host; /* TODO: Shlomi, why warning here? */


    private static IHost CreateHost(StartupEventArgs args)
    {
        IHostBuilder register = Bootstrapper
            .Register(args.Args);
        IHostBuilder hostBuilder = register
            .ConfigureServices((_, services) =>
            {
                services.AddSingletonView<MainWindowViewModel, MainWindow>();
            });

        return hostBuilder.Build();
    }

    protected override async void OnStartup(StartupEventArgs args)
    {
        host = CreateHost(args);

        await host.StartAsync(); /* TODO: Shlomi, why?? */

        host.Services.GetView<MainWindowViewModel, MainWindow>().Show();

        base.OnStartup(args);
    }
}
