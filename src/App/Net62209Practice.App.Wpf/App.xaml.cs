using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using NoNameCompany.IMS.App.Wpf.Views;
using NoNameCompany.IMS.BL.Bootstrapping;
using System.Windows;
using Serilog;
using Serilog.Events;

namespace NoNameCompany.IMS.App.Wpf;

public partial class App
{
    private IHost host; /* TODO: Shlomi, why warning here? */


    protected override async void OnStartup(StartupEventArgs args)
    {
        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args.Args)
            .AddIMSServices()
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<MainWindowViewModel>();
                builder.RegisterType<MainWindow>();
            });

        host = hostBuilder.Build();

        await host.StartAsync(); /* TODO: Shlomi, why?? */

        host.Services.GetService<MainWindow>()!.Show();

        base.OnStartup(args);
    }
}
