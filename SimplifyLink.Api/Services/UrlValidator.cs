namespace SimplifyLink.Api.Services;

public interface IUrlValidator
{
    bool IsValid(string? url);
}

public class UrlValidator : IUrlValidator
{
    public bool IsValid(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        return Uri.TryCreate(url, UriKind.Absolute, out var parsed)
               && (parsed.Scheme == Uri.UriSchemeHttp || parsed.Scheme == Uri.UriSchemeHttps);
    }
}