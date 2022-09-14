using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

public class SQLite3DAL : IDAL
{
    private readonly SQLite3ConnectionString sqLite3ConnectionString;
    private SqliteConnection? connection;


    public SQLite3DAL(SQLite3ConnectionString sqLite3ConnectionString) => 
        this.sqLite3ConnectionString = sqLite3ConnectionString;

    public bool Connect()
    {
        try
        {
            connection = new SqliteConnection(sqLite3ConnectionString.ConnectionString);
            connection.Open();

            return true;
        }
        catch (Exception exception)
        {
            /* TODO: Shlomi, log me! */
            return false;
        }
    }

    public bool CanAddItems() => true;

    public void AddItemsBulk(IEnumerable<ItemData> items)
    {
        // if (!connected)
        {
            Connect();
        }
        
    }
}