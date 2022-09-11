using System.IO.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net62209Practice.Tests.Core;
using NUnit.Framework;

namespace Net62209Practice.BL.Bootstrapping.Tests;

public class BootstrapperTest
{
    //private Bootstrapper uut;


    [SetUp]
    public void SetUp()
    {
        //uut = new Bootstrapper();
    }

    [TearDown]
    public void TearDown()
    {
        // uut = null;
    }


    [Test]
    public void Test__Register__nullArgs__Expected_UUT_ShouldNotBeNull()
    {
        IHostBuilder uut = Bootstrapper.Register(null);

        uut.Should().NotBeNull();
    }

    [Test]
    public void Test__Register__ArrayEmpty_string_Args__Expected_UUT_ShouldNotBeNull()
    {
        IHostBuilder uut = Bootstrapper.Register(Array.Empty<string>());

        uut.Should().NotBeNull();
    }


    [Test, Category(TestCategory.MustPass)]
    public void Test__Register__GetRequiredService_FileSystem__Expected_fileSystem_ShouldNotBeNull()
    {
        IHostBuilder hostBuilder = Bootstrapper.Register(null);
        IHost uut = hostBuilder.Build();

        var fileSystem = uut.Services.GetRequiredService<IFileSystem>();
        fileSystem.Should().NotBeNull();
    }
}
