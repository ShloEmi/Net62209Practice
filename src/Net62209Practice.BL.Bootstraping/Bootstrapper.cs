using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.Bootstrapping;

public static class Bootstrapper
{
    public static IHostBuilder CreateHostBuilder(string[] args, out IContainer container)
    {
        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddLogging(); /* TODO: Shlomi, Replace with Serilog */
            });

        // hostBuilder.ConfigureServices()

        IHost build = hostBuilder.Build();

        /* TODO: Shlomi, WIP TBC.. */
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();

        containerBuilder.RegisterModule<SQLite3Module>();

        containerBuilder.Populate(build.Services.GetServices().as);
        container = containerBuilder.Build();

        return hostBuilder;
    }
}