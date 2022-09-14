using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using NoNameCompany.IMS.App.Wpf.Views;
using NoNameCompany.IMS.BL.Bootstrapping;

namespace NoNameCompany.IMS.App.Wpf;

public partial class App
{
    private IHost host; /* TODO: Shlomi, why warning here? */
    /* TODO: Shlomi, Interface / abstract logging, DI and other concrete technologies! */

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
