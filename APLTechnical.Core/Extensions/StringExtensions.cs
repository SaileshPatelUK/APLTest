namespace APLTechnical.Core.Extensions;

public static class StringExtensions
{
    public static string ThrowIfNullOrWhiteSpace(
        this string? value,
        string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{paramName} must be provided.", paramName);
        }

        return value;
    }
}
