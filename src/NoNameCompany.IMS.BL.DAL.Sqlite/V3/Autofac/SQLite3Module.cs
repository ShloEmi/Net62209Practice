using Autofac;
using NoNameCompany.IMS.BL.DAL.Interfaces;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;

public class SQLite3Module : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SQLite3DALBootstrapper>()
            .As<IDAL>()
            .As<IStartable>();


        builder.RegisterType<SQLite3DAL>()
            .As<IDAL>()
            .As<IStartable>()

            .Named<IDAL>("ItemsDB")

            /* TODO: Shlomi, take me from config */
            .WithParameter("connectionString", SQLite3DAL.Defaults.ItemsDbConnectionString); 


        base.Load(builder);
    }
}