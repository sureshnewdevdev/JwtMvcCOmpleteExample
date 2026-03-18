namespace JwtMvcCOmpleteExample.Models;

/// <summary>
/// Represents the simple in-memory user used by the sample authentication flow.
/// </summary>
public sealed class ApplicationUser
{
    public required string Username { get; init; }

    public required string Password { get; init; }

    public required string DisplayName { get; init; }

    public required string Role { get; init; }
}
