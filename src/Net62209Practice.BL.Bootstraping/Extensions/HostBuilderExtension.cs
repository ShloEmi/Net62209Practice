using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.BL.Bootstrapping.Autofac;
using Serilog;

namespace NoNameCompany.IMS.BL.Bootstrapping.Extensions;

public static class HostBuilderExtension
{
    public static IHostBuilder AddIMSServices(this IHostBuilder hostBuilder) =>
        hostBuilder
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                /* TODO: Shlomi, add caller name !! */
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();
            });

    public static ContainerBuilder RegisterIMSServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule<BLModule>(); /* TODO: Shlomi, This need to be part of the Configuration - need to load Modules from there. */
        return containerBuilder;
    }
}
