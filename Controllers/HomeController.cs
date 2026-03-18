using JwtMvcCOmpleteExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtMvcCOmpleteExample.Controllers;

/// <summary>
/// Serves the home dashboard and the role-specific MVC pages.
/// </summary>
public sealed class HomeController : Controller
{
    private readonly ApiGateWay _apiGateWay;

    public HomeController(ApiGateWay apiGateWay)
    {
        _apiGateWay = apiGateWay;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var user = await _apiGateWay.GetCurrentUserAsync(cancellationToken);
        return View(user);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AdminPage() => View();

    [Authorize(Roles = "User")]
    public IActionResult UserPage() => View();

    [Authorize(Roles = "Staff")]
    public IActionResult StaffPage() => View();

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();
}
