namespace NoNameCompany.IMS.BL.DAL.SQLite.Settings;

public sealed class ItemsDataSettings
{
    public ItemsDataSettings()
    {
        ItemsDbPath = string.Empty;
        ConnectionStringArgs = string.Empty;
    }


    public string ConnectionString => 
        $"Data Source={ItemsDbPath}{(string.IsNullOrWhiteSpace(ConnectionStringArgs) ? $";{ConnectionStringArgs}" : string.Empty)}";


    public string ItemsDbPath { get; set; }
    public string ConnectionStringArgs { get; set; }
}
