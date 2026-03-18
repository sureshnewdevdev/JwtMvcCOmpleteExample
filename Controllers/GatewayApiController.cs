using System.Security.Claims;
using JwtMvcCOmpleteExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtMvcCOmpleteExample.Controllers;

/// <summary>
/// A protected API endpoint consumed by the ApiGateWay service to prove JWT-based API access.
/// </summary>
[ApiController]
[Route("api/gateway")]
[Authorize]
public sealed class GatewayApiController : ControllerBase
{
    [HttpGet("me")]
    public ActionResult<AuthenticatedUserViewModel> GetCurrentUser()
    {
        return Ok(new AuthenticatedUserViewModel
        {
            Username = User.Identity?.Name ?? string.Empty,
            DisplayName = User.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.UniqueName) ?? string.Empty,
            Role = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
        });
    }
}
