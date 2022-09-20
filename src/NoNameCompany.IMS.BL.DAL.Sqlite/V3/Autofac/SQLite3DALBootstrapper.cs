using Autofac;
using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.BL.DAL.SQLite.Settings;
using Serilog;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;

public class SQLite3DALBootstrapper : Module, IStartable
{
    private readonly IFileSystem fileSystem;
    private readonly ILogger logger;
    private readonly ItemsDataSettingsDTO itemsDataSettings;


    public SQLite3DALBootstrapper(IFileSystem fileSystem, ILogger logger, ItemsDataSettingsDTO itemsDataSettings)
    {
        this.fileSystem = fileSystem;
        this.logger = logger;
        this.itemsDataSettings = itemsDataSettings;
    }


    /// <exception cref="T:System.IO.IOException">An I/O error occurred while trying to open the file.</exception>
    public void Start()
    {
        if (CreateTablesIfDbNotExist()) 
            return;

        var message = "Couldn't open Database.";
        logger.Fatal(message);

        throw new IOException(message);
    }

    private bool CreateTablesIfDbNotExist()
    {
        /* TODO: Shlomi, move me to right place */
        //ItemsDataSettingsDTO itemsDataSettings
            //= configuration
            //.GetSection(DataLayerSectionName)
            //.Get<ItemsDataSettingsDTO>() ?? new ItemsDataSettingsDTO(SQLite3DAL.Defaults.ItemsDbConnectionString); /* TODO: Shlomi, Get<ItemsDataSettingsDTO not working! fix this... */


        if (!fileSystem.File.Exists(SQLite3DAL.Defaults.ItemsDbPath))
        {
            try
            {
                

                fileSystem.Directory.CreateDirectory(fileSystem.Path.GetDirectoryName(SQLite3DAL.Defaults.ItemsDbPath));

                using Stream stream = fileSystem.File.Create(SQLite3DAL.Defaults.ItemsDbPath);
            }
            catch (Exception exception)
            {
                logger.Error(exception, 
                    "Couldn't File.Create: '{Defaults.ItemsDbPath}'"
                    , SQLite3DAL.Defaults.ItemsDbPath);

                return false;
            }
        }


        try
        {
            using SqliteConnection connection = new(itemsDataSettings.ConnectionString);
            connection.Open();
        }
        catch (Exception exception)
        {
            logger.Error(exception, 
                "Couldn't open ConnectionString: '{itemsDataSettings.ConnectionString}'"
                , itemsDataSettings.ConnectionString);
        }


        return true; /* TODO: Shlomi,  TBC.. */
    }
}
