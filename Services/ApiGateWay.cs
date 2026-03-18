using JwtMvcCOmpleteExample.Models;

namespace JwtMvcCOmpleteExample.Services;

/// <summary>
/// Demonstrates an API gateway style service that calls a protected API endpoint
/// by forwarding the JWT token that was issued during login.
/// </summary>
public sealed class ApiGateWay
{
    private readonly HttpClient _httpClient;

    public ApiGateWay(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthenticatedUserViewModel?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<AuthenticatedUserViewModel>("/api/gateway/me", cancellationToken);
    }
}
