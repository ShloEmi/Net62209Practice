using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.Bootstrapping.Autofac;



// ReSharper disable once UnusedMember.Global
public class BLModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();

        builder.RegisterAutoMapper(typeof(BLModule).Assembly);


        builder.RegisterModule<SQLite3Module>();

        base.Load(builder);
    }
}
