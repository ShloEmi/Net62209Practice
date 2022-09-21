﻿using Microsoft.Data.Sqlite;
using NoNameCompany.IMS.BL.DAL.Framework;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.Text;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Extensions;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

/// <inheritdoc />
public class SQLite3DAL : DALBase
{
    public class Defaults
    {
        public static readonly string ItemsDbPath = @".\sqlite3\Items.db";
        public static readonly string ItemsDbConnectionString = @$"Data Source={ItemsDbPath};Mode=ReadWrite";
    }


    private readonly ILogger logger;


    /* TODO: Shlomi, connectionString?  */
    public SQLite3DAL(ILogger logger)
    {
        this.logger = logger;
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

            using SqliteConnection connection = new(Defaults.ItemsDbConnectionString);
            connection.Open();
            

            StringBuilder commandBuilder = new ();
            using (commandBuilder.BeginTransaction())
                foreach (ItemData itemData in itemDatum)
                {

                    commandBuilder.AppendLine("INSERT INTO 'Items' table VALUES ('data1', 'data2');");
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
