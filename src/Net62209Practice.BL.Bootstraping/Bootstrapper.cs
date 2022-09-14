using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.Bootstrapping;

public static class Bootstrapper
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        IHostBuilder hostBuilder = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IFileSystem, FileSystem>();

                services.RegisterSQLite3(); /* TODO: Shlomi, using (var connection = new SqliteConnection("Data Source=hello.db")) */
            });

        return hostBuilder;
    }
}