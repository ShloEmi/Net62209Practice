using FluentAssertions;
using NUnit.Framework;

namespace TestProjectExample;

public class ExampleClassTests
{
    /* TODO: This is an example development leftover */
    // REMARK: This is an example remark leftover

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // will happen once per all tests
    }
    
    [SetUp]
    public void Setup()
    {
        // will happen before test
    }

    [TearDown]
    public void TearDown()
    {
        // will happen after test
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // will happen once after all tests
    }


    [Test, Ignore("Because ignored")]
    public void Test__TestIgnored__Ignore__ExpectedTestNotRunning() => 
        false.Should().Be(true);

    [Test]
    public void Test__TestPass__Nothing__ExpectedPass()
    {
        true
            .Should()
            .Be(true);
    }
}