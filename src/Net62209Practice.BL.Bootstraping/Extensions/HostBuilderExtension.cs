using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace NoNameCompany.IMS.BL.Bootstrapping.Extensions;

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