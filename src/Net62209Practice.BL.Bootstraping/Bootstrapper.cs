using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.Abstractions;

namespace Net62209Practice.BL.Bootstrapping;

public class Bootstrapper
{
    public static IHostBuilder Register(string[] args)
    {
        IHostBuilder builder = Host
            .CreateDefaultBuilder(args);

        IHostBuilder configureServices = builder
            .ConfigureServices((_, services) => services.AddSingleton<IFileSystem, FileSystem>());

        return configureServices;
    }
}