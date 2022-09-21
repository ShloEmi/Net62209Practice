using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;

internal record ItemDataSqlite3DTO : ItemData
{
    public string ToSqlInsert()
    {
        return string.Empty; /* TODO: Shlomi, TBC ..  */
    }
}