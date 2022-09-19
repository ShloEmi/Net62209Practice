using Autofac;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Framework;
using NoNameCompany.IMS.BL.DAL.SQLite.Settings;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.IO.Abstractions;
using System.Text;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

/// <inheritdoc />
public class SQLite3DAL : DALBase, IStartable
{
    public class Defaults
    {
        public static readonly string ItemsDbPath = @".\sqlite3\Items.db";
        public static readonly string ItemsDbConnectionString = @$"Data Source={ItemsDbPath};Mode=ReadWrite";
    }


    private readonly string connectionString;
    private readonly ILogger logger;
    private readonly IFileSystem fileSystem;
    private readonly IConfiguration configuration;


    public SQLite3DAL(string connectionString, ILogger logger, IFileSystem fileSystem, IConfiguration configuration)
    {
        this.connectionString = connectionString;
        this.logger = logger;
        this.fileSystem = fileSystem;
        this.configuration = configuration;
    }


    /// <exception cref="T:System.IO.IOException">An I/O error occurred while trying to open the file.</exception>
    public void Start()
    {
        if (CreateTablesIfDbNotExist() == false)
        {
            var message = "Couldn't open Database.";
            logger.Fatal(message);

            throw new IOException(message);
        }

        
        ;   /*TODO: Shlomi.O, TBC..         */
    }

    private bool CreateTablesIfDbNotExist()
    {
        ItemsDataSettingsDTO itemsDataSettings = configuration
            .GetSection(DataLayerSectionName)
            .Get<ItemsDataSettingsDTO>() ?? new ItemsDataSettingsDTO(Defaults.ItemsDbConnectionString); /* TODO: Shlomi, Get<ItemsDataSettingsDTO not working! fix this... */

        if (!fileSystem.File.Exists(Defaults.ItemsDbPath))
        {
            try
            {
                fileSystem.File.Create(Defaults.ItemsDbPath);
            }
            catch (Exception exception)
            {
                logger.Error(exception, 
                    "Couldn't File.Create: '{Defaults.ItemsDbPath}'"
                    , Defaults.ItemsDbPath);

                return false;
            }
        }


        try
        {
            using SqliteConnection connection = GetConnection();
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


    public override bool CanAddItems() => true;

    public override bool AddItemsBulk(IEnumerable<ItemData>? items)
    {
        if (items == null)
            return true;

        ItemData[] itemDatum = items as ItemData[] ?? items.ToArray();
        if (!itemDatum.Any())
            return true;


        try
        {
            logger.Information("Try AddItemsBulk, count: {itemDatum-Count}", itemDatum.Length);

            using SqliteConnection connection = GetConnection();
            connection.Open();
            

            StringBuilder commandBuilder = new ();
            commandBuilder.AppendLine("BEGIN TRANSACTION;");
            foreach (ItemData itemData in itemDatum)
            {
                /* TODO: Shlomi, compare native sql vs Sqlite3Framework */
                commandBuilder.AppendLine("INSERT INTO 'Items' table VALUES ('data1', 'data2');");
            }
            commandBuilder.AppendLine("COMMIT;");


            SqliteCommand command = connection.CreateCommand();
            command.CommandText = commandBuilder.ToString();
            
            // command.Parameters.AddWithValue("$id", id);

            return command.ExecuteNonQuery() == itemDatum.Length;
        }
        catch (Exception exception)
        {
            logger.Error(exception, string.Empty);
            return false;
        }
    }

    private SqliteConnection GetConnection() => 
        new(connectionString);
}