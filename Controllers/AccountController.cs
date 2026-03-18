using JwtMvcCOmpleteExample.Models;
using JwtMvcCOmpleteExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtMvcCOmpleteExample.Controllers;

/// <summary>
/// Handles the login and logout workflow for the sample users.
/// </summary>
[AllowAnonymous]
public sealed class AccountController : Controller
{
    private readonly AuthendicationService _authendicationService;

    public AccountController(AuthendicationService authendicationService)
    {
        _authendicationService = authendicationService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.ErrorMessage = "Enter both user name and password.";
            return View(model);
        }

        var token = _authendicationService.SignIn(model.Username, model.Password, out var expiresUtc);
        if (token is null)
        {
            model.ErrorMessage = "Invalid credentials. Try admin/Admin@123, user/User@123 or staff/Staff@123.";
            return View(model);
        }

        _authendicationService.WriteTokenCookie(Response, token, expiresUtc);
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        _authendicationService.SignOut(Response);
        return RedirectToAction(nameof(Login));
    }
}
