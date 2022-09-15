using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using NoNameCompany.IMS.App.Wpf.Views;
using NoNameCompany.IMS.BL.Bootstrapping;
using System.Windows;

namespace NoNameCompany.IMS.App.Wpf;

public partial class App
{
    private IHost host; /* TODO: Shlomi, why warning here? */


    protected override async void OnStartup(StartupEventArgs args)
    {
        string appSettingsName = "appSettings", jsonFileExtension = "json";

        //IConfiguration config = new ConfigurationBuilder()
        //    .AddJsonFile("appSettingsName.json")
        //    .AddEnvironmentVariables()
        //    .Build();

        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args.Args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                configuration
                    .AddJsonFile($"{appSettingsName}.{jsonFileExtension}", true, reloadOnChange: true)
                    .AddJsonFile($"{appSettingsName}.{hostingContext.HostingEnvironment.EnvironmentName}.{jsonFileExtension}", true, reloadOnChange: true);

                // IConfigurationRoot configurationRoot = configuration.Build();
            })

            .AddIMSServices()
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<MainWindowViewModel>();
                builder.RegisterType<MainWindow>();
            });

        host = hostBuilder.Build();

        // config = host.Services.GetRequiredService<IConfiguration>();


        await host.StartAsync(); /* TODO: Shlomi, why?? */

        host.Services.GetService<MainWindow>()!.Show();

        base.OnStartup(args);
    }
}
