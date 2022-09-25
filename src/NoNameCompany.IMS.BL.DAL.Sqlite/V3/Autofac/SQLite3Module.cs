using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using NoNameCompany.IMS.BL.DAL.Interfaces;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;

// ReSharper disable once UnusedMember.Global
public class SQLite3Module : Module
{

    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<SQLite3DALBootstrapper>()
            .AsSelf()
            .As<IStartable>();


        builder.RegisterType<SQLite3DAL>()
            .As<IDAL>()
            .Named<IDAL>("ItemsDB");


        builder.RegisterAutoMapper(typeof(SQLite3Module).Assembly);

        base.Load(builder);
    }
}
