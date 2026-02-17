using SimplifyLink.Api.Services;
using Xunit;

namespace SimplifyLink.Tests;

public class ShortCodeGeneratorTests
{
    [Fact]
    public void Generate_ReturnsCorrectLength()
    {
        IShortCodeGenerator gen = new ShortCodeGenerator();
        var code = gen.Generate(6);
        Assert.Equal(6, code.Length);
    }

    [Fact]
    public void Generate_TwoCalls_ShouldUsuallyDiffer()
    {
        IShortCodeGenerator gen = new ShortCodeGenerator();
        var a = gen.Generate(6);
        var b = gen.Generate(6);
        Assert.NotEqual(a, b);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(13)]
    public void Generate_InvalidLength_Throws(int length)
    {
        IShortCodeGenerator gen = new ShortCodeGenerator();
        Assert.Throws<ArgumentOutOfRangeException>(() => gen.Generate(length));
    }
}