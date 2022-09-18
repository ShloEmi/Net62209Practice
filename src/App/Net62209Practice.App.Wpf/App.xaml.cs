using System;
using Autofac;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using NoNameCompany.IMS.App.Wpf.Views;
using NoNameCompany.IMS.BL.Bootstrapping;
using NoNameCompany.IMS.Data.ApplicationData;
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
                builder.Register<Faker<ItemData>>((context, parameters) =>
                {
                    return new Faker<ItemData>()
                        //Ensure all properties have rules. By default, StrictMode is false
                        //Set a global policy by using Faker.DefaultStrictMode
                        .StrictMode(true)
                        //OrderId is deterministic
                        .RuleFor(o => o.Id, f => Guid.NewGuid())
                        .RuleFor(o => o.Name, f => f.Name.JobTitle())
                        .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                        .RuleFor(o => o.ItemCategorization, f =>
                            {
                                ItemCategorizationData itemCategorizationData = new Faker<ItemCategorizationData>()
                                    .RuleFor(o => o.Id, f1 => Guid.NewGuid())
                                    .RuleFor(o => o.Name, f2 => f2.Name.JobTitle())
                                    .RuleFor(o => o.Description, f3 => f3.Lorem.Sentence()).Generate();
                                return itemCategorizationData;
                            }
                        );
                });
                builder.RegisterType<MainWindowViewModel>();
                builder.RegisterType<MainWindow>();
            });

        host = hostBuilder.Build();
        
        await host.StartAsync(); /* TODO: Shlomi, why?? */

        host.Services.GetService<MainWindow>()!.Show();

        base.OnStartup(args);
    }
}
