namespace JwtMvcCOmpleteExample.Models;

/// <summary>
/// Provides strongly typed access to the JWT settings in configuration.
/// </summary>
public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public required string SecretKey { get; init; }
}
