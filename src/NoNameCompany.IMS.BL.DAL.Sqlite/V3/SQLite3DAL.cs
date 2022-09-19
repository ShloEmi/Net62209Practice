using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.BL.DAL.Framework;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.Text;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

/// <inheritdoc />
public class SQLite3DAL : DALBase
{
    public class Defaults
    {
        public static readonly string ItemsDbPath = @".\sqlite3\Items.db";
        public static readonly string ItemsDbConnectionString = @$"Data Source={ItemsDbPath};Mode=ReadWrite";
    }


    private readonly string connectionString;
    private readonly ILogger logger;


    public SQLite3DAL(string connectionString, ILogger logger)
    {
        this.connectionString = connectionString; /* TODO: Shlomi, get relevant section from the config */
        this.logger = logger;
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
