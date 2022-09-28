using Autofac;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Settings;
using Serilog;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;

public class SQLite3DALBootstrapper : Module, IStartable
{
    private readonly IFileSystem fileSystem;
    private readonly ILogger logger;
    private readonly ItemsDataSettings itemsDataSettings = new();


    public SQLite3DALBootstrapper(IFileSystem fileSystem, ILogger logger, IConfiguration configuration)
    {
        this.fileSystem = fileSystem;
        this.logger = logger;

        configuration
            .GetSection(nameof(ItemsDataSettings))
            .Bind(itemsDataSettings);
    }


    /// <exception cref="T:System.IO.IOException">An I/O error occurred while trying to open the file.</exception>
    public void Start()
    {
        if (!itemsDataSettings.Version.StartsWith("3"))
            throw new Exception("Unsupported version");


        if (CreateTablesIfNeeded()) 
            return;

        var message = "Couldn't open Database.";
        logger.Fatal(message);

        throw new IOException(message);
    }

    private bool CreateTablesIfNeeded()
    {
        /* TODO: Shlomi, move me to right place */
        //ItemsDataSettingsDTO itemsDataSettings
        //= configuration
        //.GetSection(DataLayerSectionName)
        //.Get<ItemsDataSettingsDTO>() ?? new ItemsDataSettingsDTO(SQLite3DAL.Defaults.ItemsDbConnectionString); /* TODO: Shlomi, Get<ItemsDataSettingsDTO not working! fix this... */
        
        try
        {
            if (!fileSystem.File.Exists(itemsDataSettings.ItemsDbPath))
            {
                fileSystem.Directory.CreateDirectory(fileSystem.Path.GetDirectoryName(itemsDataSettings.ItemsDbPath));

                using Stream stream = fileSystem.File.Create(itemsDataSettings.ItemsDbPath);
            }
        }
        catch (Exception exception)
        {
            logger.Fatal(exception, 
                "Couldn't File.Create: '{Defaults.ItemsDbPath}'"
                , itemsDataSettings.ItemsDbPath);

            return false;
        }


        try
        {
            using SqliteConnection connection = new(itemsDataSettings.ConnectionString);
            connection.Open();

            /* TODO: Shlomi, TBC - create/define tables!! */
            MakeDataTables(connection);
        }
        catch (Exception exception)
        {
            logger.Fatal(exception, 
                "Couldn't open ConnectionString: '{itemsDataSettings.ConnectionString}'"
                , itemsDataSettings.ConnectionString);

            return false;
        }

        return true;
    }


    private void MakeDataTables(SqliteConnection connection)
    {
        foreach (string dbScript in fileSystem.Directory.EnumerateFiles(Path.Combine(".", "Sqlite3"), "*.sql"))
        {
            var readAllLines = fileSystem.File.ReadAllText(dbScript);

            SqliteCommand sqliteCommand = connection.CreateCommand();
            sqliteCommand.CommandText = readAllLines;
            var executeNonQuery = sqliteCommand.ExecuteNonQuery();
        }
    }
}
