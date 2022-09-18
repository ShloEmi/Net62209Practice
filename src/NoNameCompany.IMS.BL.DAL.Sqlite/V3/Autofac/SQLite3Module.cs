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
            /*.Named<IDAL>("ItemsTable")*/
            .WithParameter("connectionString", "Data Source=Items.db");
        //builder.RegisterType<SQLite3DAL>().As<IDAL>().Named<IDAL>("AnotherTable")
        //    .WithParameter("connectionString", "Data Source=Another.db");

        base.Load(builder);
    }
}