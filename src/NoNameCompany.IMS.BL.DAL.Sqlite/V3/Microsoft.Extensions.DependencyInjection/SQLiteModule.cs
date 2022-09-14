using Microsoft.Extensions.DependencyInjection;
using NoNameCompany.IMS.BL.DAL.Interfaces;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Microsoft.Extensions.DependencyInjection;

public static class SQLiteModule
{
    public static void RegisterSQLite3(this IServiceCollection services)
    {
        services.AddTransient<IDAL, SQLite3DAL>();
        services.AddTransient<IDALConnectionString, SQLite3ConnectionString>( /* TODO: Shlomi, which one? */);
    }
}