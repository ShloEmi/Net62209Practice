using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Framework;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Settings;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.Reactive.Subjects;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

/// <inheritdoc />
public class SQLite3DAL : DALBase
{
    private readonly ILogger logger;
    private readonly IMapper mapper;    /* TODO: Shlomi, add auto-wire support! */
    private readonly IConfiguration configuration;
    private readonly ItemsDataSettings itemsDataSettings = new();


    /* TODO: Shlomi, connectionString?  */
    public SQLite3DAL(ILogger logger, IMapper mapper, IConfiguration configuration)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.configuration = configuration;

        configuration
            .GetSection(nameof(ItemsDataSettings))
            .Bind(itemsDataSettings);
        

        ItemsChanged = itemsChanged = new Subject<IEnumerable<ItemChanged>>();

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
            

            using SqliteCommand command = connection.CreateCommand();
            command.ToSqlInsert(mapper, itemDatum);
            
            if (command.ExecuteNonQuery() != itemDatum.Length)  /* TODO: Shlomi, use transaction + rollback! */
                return false;

            
            itemsChanged.OnNext(itemDatum.Select(data => new ItemChanged(data, ChangeDescriptions.added)));
            return true;

        }
        catch (Exception exception)
        {
            logger.Error(exception, string.Empty);
            return false;
        }
    }

    private string GetSqliteConnection() =>
        $"Data Source={itemsDataSettings.ItemsDbPath}" +
        $"{(string.IsNullOrWhiteSpace(itemsDataSettings.ConnectionStringArgs) ? string.Empty : $";{itemsDataSettings.ConnectionStringArgs}")}";


    private readonly ISubject<IEnumerable<ItemChanged>> itemsChanged;
    public override IObservable<IEnumerable<ItemChanged>> ItemsChanged { get; }
}
