namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Settings;

public sealed class ItemsDataSettings
{
    public ItemsDataSettings()
    {
        Version = string.Empty;
        ItemsDbPath = string.Empty;
        ConnectionStringArgs = string.Empty;
    }


    public string ConnectionString => 
        $"Data Source={ItemsDbPath}{(!string.IsNullOrWhiteSpace(ConnectionStringArgs) ? $";{ConnectionStringArgs}" : string.Empty)}";


    public string Version { get; set; }
    public string ItemsDbPath { get; set; }
    public string ConnectionStringArgs { get; set; }
}
