using AutoMapper;
using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.Data.ApplicationData;
using System.Text;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;

internal static class ItemDataSqlite3DTOExt
{
    public static SqliteCommand ToSqlInsert(this SqliteCommand command, IMapper mapper, ItemData[] itemDatum)
    {
        StringBuilder sb = new(itemDatum.Length * 128 + 128);
        sb.AppendLine(@"INSERT INTO ITEM_DATA ( NAME, DESCRIPTION )  VALUES ");
        sb.AppendLine(string.Join(",  " + Environment.NewLine, itemDatum.Select((data, i) => @$"( $Name{i}, $Description{i} )")));
        sb.AppendLine(";");

        var i = 0;
        foreach (ItemData data in itemDatum)
        {
            command.Parameters.AddWithValue($"$Name{i}", data.Name);    /* TODO: Shlomi, $ItemCategorization */
            command.Parameters.AddWithValue($"$Description{i}", data.Description);
            ++i;
        }
        
        command.CommandText = sb.ToString();
        return command;
    }
}
