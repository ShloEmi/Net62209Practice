using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.Abstractions;

namespace Net62209Practice.BL.Bootstrapping;

public static class Bootstrapper
{
    public static IHostBuilder Register(string[] args)
    {
        IHostBuilder defaultBuilder = Host
            .CreateDefaultBuilder(args);
        IHostBuilder hostBuilder = defaultBuilder
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IFileSystem, FileSystem>();
            });

        return hostBuilder;
    }
}