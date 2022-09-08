using FluentAssertions;
using NUnit.Framework;

namespace TestProjectExample
{
    public class ExampleClassTests
    {
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
}
