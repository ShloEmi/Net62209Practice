using Autofac;
using NoNameCompany.IMS.BL.DAL.Interfaces;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;

public class SQLite3Module : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SQLite3DAL>()
            .As<IDAL>()
            .As<IStartable>()

            .Named<IDAL>("ItemsDB")

            .WithParameter("connectionString", @"Data Source=.\sqlite3\Items.db;Version=3;FailIfMissing=False"); 


        base.Load(builder);
    }
}