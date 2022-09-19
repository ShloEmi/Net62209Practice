using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
}
