using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Framework;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Extensions;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.Text;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Settings;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

/// <inheritdoc />
public class SQLite3DAL : DALBase
{
    private readonly ILogger logger;
    private readonly IMapper mapper;    /* TODO: Shlomi, add auto-wire support! */
    private readonly ItemsDataSettings itemsDataSettings;


    /* TODO: Shlomi, connectionString?  */
    public SQLite3DAL(ILogger logger, IMapper mapper, IConfiguration configuration)
    {
        this.logger = logger;
        this.mapper = mapper;

        configuration
            .GetSection(nameof(ItemsDataSettings))
            .Bind(itemsDataSettings);
    }
    

    public override bool CanAddItems() => true;

    /// <remarks> Thread safe </remarks>
    public override bool AddItemsBulk(IEnumerable<ItemData>? items)
    {
        if (items == null)
            return true;

        ItemData[] itemDatum = items as ItemData[] ?? items.ToArray();
        if (!itemDatum.Any())
            return true;


        try
        {
            logger.Information("Try AddItemsBulk, count: {itemDatum-Count}", itemDatum.Length);

            using SqliteConnection connection = new(itemsDataSettings.ConnectionString);
            connection.Open();
            

            StringBuilder commandBuilder = new ();
            using (commandBuilder.BeginTransaction())
                foreach (ItemData itemData in itemDatum)
                {
                    var itemDataSqliteDTO = mapper.Map<ItemDataSqlite3DTO>(itemData);
                    string sqlInsert = itemDataSqliteDTO.ToSqlInsert();
                    commandBuilder.AppendLine($"INSERT INTO 'Items' table VALUES ({sqlInsert});");
                }

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = commandBuilder.ToString();
            
            // command.Parameters.AddWithValue("$id", id);
            return command.ExecuteNonQuery() == itemDatum.Length;
        }
        catch (Exception exception)
        {
            logger.Error(exception, string.Empty);
            return false;
        }
    }
}
