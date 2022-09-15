using System.IO.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoNameCompany.IMS.Tests.Core;
using NUnit.Framework;

namespace NoNameCompany.IMS.BL.Bootstrapping.Tests;

public class BootstrapperTest
{
    //private HostBuilderExtension uut;


    [SetUp]
    public void SetUp()
    {
        //uut = new HostBuilderExtension();
    }

    [TearDown]
    public void TearDown()
    {
        // uut = null;
    }


    [Test]
    public void Test__Register__nullArgs__Expected_UUT_ShouldNotBeNull()
    {
        IHostBuilder uut = HostBuilderExtension.AddIMSServices(Host
            .CreateDefaultBuilder(null));

        uut.Should().NotBeNull();
    }

    [Test]
    public void Test__Register__ArrayEmpty_string_Args__Expected_UUT_ShouldNotBeNull()
    {
        string[] args = Array.Empty<string>();
        IHostBuilder uut = HostBuilderExtension.AddIMSServices(Host
            .CreateDefaultBuilder(args));

        uut.Should().NotBeNull();
    }


    [Test, Category(TestCategory.MustPass)]
    public void Test__Register__GetRequiredService_FileSystem__Expected_fileSystem_ShouldNotBeNull()
    {
        IHostBuilder hostBuilder = HostBuilderExtension.AddIMSServices(Host
            .CreateDefaultBuilder(null));
        IHost uut = hostBuilder.Build();

        var fileSystem = uut.Services.GetService<IFileSystem>();
        fileSystem.Should().NotBeNull();
    }
}
