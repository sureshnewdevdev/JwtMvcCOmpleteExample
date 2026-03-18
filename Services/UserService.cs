using JwtMvcCOmpleteExample.Models;

namespace JwtMvcCOmpleteExample.Services;

/// <summary>
/// Exposes sample users for the Admin, User, and Staff roles.
/// In a production application this would read from a database or identity provider.
/// </summary>
public sealed class UserService
{
    private static readonly IReadOnlyList<ApplicationUser> Users =
    [
        new ApplicationUser { Username = "admin", Password = "Admin@123", DisplayName = "Application Admin", Role = "Admin" },
        new ApplicationUser { Username = "user", Password = "User@123", DisplayName = "Application User", Role = "User" },
        new ApplicationUser { Username = "staff", Password = "Staff@123", DisplayName = "Operations Staff", Role = "Staff" }
    ];

    public ApplicationUser? ValidateCredentials(string username, string password)
    {
        return Users.FirstOrDefault(user =>
            user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.Password == password);
    }

    public IReadOnlyList<ApplicationUser> GetAllUsers() => Users;
}
