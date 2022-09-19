namespace NoNameCompany.IMS.BL.DAL.SQLite.Settings;

public sealed class ItemsDataSettingsDTO /* TODO: Shlomi, make a base class in framework */
{
    public static ItemsDataSettingsDTO Default = new("ItemsData.db", @"mydb.db;Version=3;FailIfMissing=True"); /* TODO: Shlomi, TBD */


    public ItemsDataSettingsDTO(string dbFilePath, string connectionString)
    {
        DbFilePath = dbFilePath;
        ConnectionString = connectionString;
    }


    public string DbFilePath { get; set; }
    public string ConnectionString { get; set; }
}