namespace SimplifyLink.Api.Services;

public interface IShortCodeGenerator
{
    string Generate(int length = 6);
}

public class ShortCodeGenerator : IShortCodeGenerator
{
    public string Generate(int length = 6)
    {
        if (length < 4 || length > 12)
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be between 4 and 12.");

        return Guid.NewGuid().ToString("N")[..length];
    }
}