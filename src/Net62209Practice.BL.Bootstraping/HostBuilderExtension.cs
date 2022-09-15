using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;
using Serilog;
using Serilog.Events;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.Bootstrapping;

public static class HostBuilderExtension
{
    public static IHostBuilder AddIMSServices(this IHostBuilder hostBuilder)
    {
        // Configure Logger
        //Log.Logger = new LoggerConfiguration()
        //    //.MinimumLevel.Debug()
        //    //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        //    //.Enrich.FromLogContext()
        //    //.WriteTo.Console()
        //    .CreateLogger();


        return hostBuilder
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
                builder.RegisterModule<SQLite3Module>();
            })
            .ConfigureServices((_, services) => { services.AddLogging(); /* TODO: Shlomi, Replace with Serilog */ })
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    //.WriteTo.Console()
                    ;
            });
    }
}