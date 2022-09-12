using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.Abstractions;

namespace Net62209Practice.BL.Bootstrapping;

public static class Bootstrapper
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IFileSystem, FileSystem>();
            });

        return hostBuilder;
    }
}