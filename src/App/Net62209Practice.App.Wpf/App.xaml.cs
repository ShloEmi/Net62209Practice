using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.App.Wpf.Extensions;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using NoNameCompany.IMS.App.Wpf.Views;
using NoNameCompany.IMS.BL.Bootstrapping.Extensions;
using System;
using System.Linq;
using System.Windows;

namespace NoNameCompany.IMS.App.Wpf;


public partial class App
{
    private IHost? host;


    /// <inheritdoc />
    protected override async void OnStartup(StartupEventArgs args)
    {
        string appSettingsName = "appSettings", jsonFileExtension = "json";

        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args.Args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                /* TODO: Shlomi, each module needs to do this for it's configuration! */
                configuration
                    .AddJsonFile($"{appSettingsName}.{jsonFileExtension}", true, reloadOnChange: true)
                    .AddJsonFile($"{appSettingsName}.{hostingContext.HostingEnvironment.EnvironmentName}.{jsonFileExtension}", true, reloadOnChange: true);
                //.AddEnvironmentVariables();
            })

            .AddIMSServices()
        
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterIMSServices();

                builder.RegisterItemDataProvider();

                /* TODO: Shlomi, move me to NoNameCompany.IMS.Wpf.Module */
                builder.RegisterType<MainWindowViewModel>()
                    .AsSelf()
                    .As<IStartable>()
                    .As<IDisposable>()

                    .SingleInstance();

                builder.RegisterType<MainWindow>().AsSelf();
            });

        try
        {
            host = hostBuilder.Build();
        }
        catch (Exception exception)
        {
            if (!args.Args.Any(s => s.Contains("UIMode=silent")))
                MessageBox.Show(exception.Message, "Error while loading the host application.",
                    MessageBoxButton.OKCancel, MessageBoxImage.Stop);

            Environment.Exit((int)ExitCodes.ExitCodeHostBuildError);
        }
        
        await host.StartAsync();

        host.Services.GetService<MainWindow>()!.Show();

        base.OnStartup(args);
    }
}

/// <ExitCodeHostBuildError> Exit with code 1 when Host-Build-Error occurred. </ExitCodeHostBuildError>
public enum ExitCodes { ExitCodeHostBuildError = 1 }
