namespace JwtMvcCOmpleteExample.Models;

/// <summary>
/// View model returned by the protected API and shown on the MVC pages.
/// </summary>
public sealed class AuthenticatedUserViewModel
{
    public string Username { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;

    public DateTime ExpiresUtc { get; init; }
}
