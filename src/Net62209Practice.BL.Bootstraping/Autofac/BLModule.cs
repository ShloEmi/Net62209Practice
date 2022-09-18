using Autofac;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.Autofac;
using System.IO.Abstractions;

namespace NoNameCompany.IMS.BL.Bootstrapping.Autofac;

public class BLModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();

        builder.RegisterModule<SQLite3Module>();

        base.Load(builder);
    }
}
