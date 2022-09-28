using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;

internal record ItemDataSqlite3DTO : ItemData
{
}

internal static class ItemDataSqlite3DTOExt
{
    public static SqliteCommand ToSqlInsert(this SqliteCommand command, ItemDataSqlite3DTO data)
    {
        /* TODO: Shlomi, $ItemCategorization */

        command.CommandText = @"
INSERT INTO 'Items' table
    (Id, Name, Description, ItemCategorization) 
    VALUES($Id, $Name, $Description);
";
        command.Parameters.AddWithValue("$Id", data.Id);
        command.Parameters.AddWithValue("$Name", data.Name);
        command.Parameters.AddWithValue("$Description", data.Description);

        return command;
    }
}