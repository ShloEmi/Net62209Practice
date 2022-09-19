namespace NoNameCompany.IMS.BL.DAL.SQLite.Settings;

public sealed class ItemsDataSettingsDTO
{
    public ItemsDataSettingsDTO(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public string ConnectionString { get; set; }
}