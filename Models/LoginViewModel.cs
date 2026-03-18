using System.ComponentModel.DataAnnotations;

namespace JwtMvcCOmpleteExample.Models;

/// <summary>
/// Captures the credentials submitted from the login screen.
/// </summary>
public sealed class LoginViewModel
{
    [Required]
    [Display(Name = "User name")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }
}
