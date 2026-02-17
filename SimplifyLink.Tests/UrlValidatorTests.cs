using SimplifyLink.Api.Services;
using Xunit;

namespace SimplifyLink.Tests;

public class UrlValidatorTests
{
    [Theory]
    [InlineData("http://google.com")]
    [InlineData("https://example.com/path?q=1")]
    public void IsValid_ValidUrls_ReturnsTrue(string url)
    {
        IUrlValidator v = new UrlValidator();
        Assert.True(v.IsValid(url));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("google.com")]
    [InlineData("ftp://example.com")]
    [InlineData("javascript:alert(1)")]
    public void IsValid_InvalidUrls_ReturnsFalse(string? url)
    {
        IUrlValidator v = new UrlValidator();
        Assert.False(v.IsValid(url));
    }
}