using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Framework;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

/// <inheritdoc />
public class SQLite3DAL : DALBase
{
    private readonly ILogger logger;
    private readonly IMapper mapper;    /* TODO: Shlomi, add auto-wire support! */
    private readonly IConfiguration configuration;


    /* TODO: Shlomi, connectionString?  */
    public SQLite3DAL(ILogger logger, IMapper mapper, IConfiguration configuration)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.configuration = configuration;

        // string str = configuration.GetValue<string>("ItemsDataSettings:ConnectionStringArgs"); /* TODO: Shlomi, Need to learn about .Net6-configuration!!! */
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


            using SqliteConnection connection = new(GetSqliteConnection());
            connection.Open();
            

            SqliteCommand command = connection.CreateCommand();
            foreach (ItemData itemData in itemDatum) 
                command.ToSqlInsert(mapper.Map<ItemDataSqlite3DTO>(itemData));

            return command.ExecuteNonQuery() == itemDatum.Length;
        }
        catch (Exception exception)
        {
            logger.Error(exception, string.Empty);
            return false;
        }
    }

    private string GetSqliteConnection()
    {
        var connectionStringArgs = configuration.GetValue<string>("ItemsDataSettings:ConnectionStringArgs");
        var itemsDbPath = configuration.GetValue<string>("ItemsDataSettings:ItemsDbPath");
        var connectionString = $"Data Source={itemsDbPath}{(string.IsNullOrWhiteSpace(connectionStringArgs) ? string.Empty : $";{connectionStringArgs}")}";
        return connectionString;
    }
}
