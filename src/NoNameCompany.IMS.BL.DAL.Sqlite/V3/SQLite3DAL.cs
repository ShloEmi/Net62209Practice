using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;
using System.Text;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

public class SQLite3DAL : IDAL
{
    private readonly string connectionString;


    public SQLite3DAL(string connectionString) => 
        this.connectionString = connectionString;



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
            /* TODO: Shlomi, log me! */
            return false;
        }
    }

    private SqliteConnection GetConnection() => 
        new(connectionString);
}