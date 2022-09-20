using Autofac;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.BL.DAL.SQLite.Settings;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;

public class SQLite3Module : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ItemsDataSettingsDTO>()
            .AsSelf()
            .WithParameter("connectionString", SQLite3DAL.Defaults.ItemsDbConnectionString); 

        builder.RegisterType<SQLite3DALBootstrapper>()
            .AsSelf()
            .As<IStartable>();


        builder.RegisterType<SQLite3DAL>()
            .As<IDAL>()

            .Named<IDAL>("ItemsDB")

            /* TODO: Shlomi, take me from config */
            .WithParameter("connectionString", SQLite3DAL.Defaults.ItemsDbConnectionString); 


        base.Load(builder);
    }
}