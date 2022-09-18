using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.App.Wpf.Extensions;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using NoNameCompany.IMS.App.Wpf.Views;
using NoNameCompany.IMS.BL.Bootstrapping.Autofac;
using NoNameCompany.IMS.BL.Bootstrapping.Extensions;
using System.Windows;

namespace NoNameCompany.IMS.App.Wpf;

public partial class App
{
    private IHost host; /* TODO: Shlomi, why warning here? */


    protected override async void OnStartup(StartupEventArgs args)
    {
        string appSettingsName = "appSettings", jsonFileExtension = "json";

        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args.Args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                configuration
                    .AddJsonFile($"{appSettingsName}.{jsonFileExtension}", true, reloadOnChange: true)
                    .AddJsonFile($"{appSettingsName}.{hostingContext.HostingEnvironment.EnvironmentName}.{jsonFileExtension}", true, reloadOnChange: true);
            })

            .AddIMSServices()
        

            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule<BLModule>();

                builder.RegisterItemDataProvider();

                builder.RegisterType<MainWindowViewModel>();
                builder.RegisterType<MainWindow>();
            });

        host = hostBuilder.Build();
        
        await host.StartAsync();

        /* TODO: Shlomi, ensure db */

        host.Services.GetService<MainWindow>()!.Show();

        base.OnStartup(args);
    }
}
