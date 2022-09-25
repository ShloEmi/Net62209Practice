namespace NoNameCompany.IMS.BL.DAL.SQLite.Settings;

public sealed class ItemsDataSettings
{
    public ItemsDataSettings() => 
        ConnectionString = string.Empty;


    public string ConnectionString { get; set; }
}
