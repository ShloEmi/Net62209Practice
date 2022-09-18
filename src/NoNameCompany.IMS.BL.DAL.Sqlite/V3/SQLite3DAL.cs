using Autofac;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.IO.Abstractions;
using System.Text;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

public sealed class ItemsDataSettings
{
    public int KeyOne { get; set; }
    public bool KeyTwo { get; set; }
    // public NestedSettings KeyThree { get; set; } = null!;
}


public class SQLite3DAL : IDAL, IStartable
{
    private readonly string connectionString;
    private readonly ILogger logger;
    private readonly IFileSystem fileSystem;
    private readonly IConfiguration configuration;
    // private readonly string dbFilePath = "ItemsData.db";


    public SQLite3DAL(string connectionString, ILogger logger, IFileSystem fileSystem, IConfiguration configuration)
    {
        this.connectionString = connectionString;
        this.logger = logger;
        this.fileSystem = fileSystem;
        this.configuration = configuration;
    }


    public void Start()
    {
        CreateTablesIfDbNotExist();
    }

    private void CreateTablesIfDbNotExist()
    {
        //var itemsDataSettings = configuration.GetRequiredSection("ItemsDataSettings").Get<ItemsDataSettings>();
        /* TODO: Shlomi, TBC... */
        //itemsDataSettings.DbFilePath

        //if (fileSystem.File.Exists(dbFilePath) == false)
        //{

        //}
    }




    public bool CanAddItems() => true;

    public bool AddItemsBulk(IEnumerable<ItemData>? items)
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