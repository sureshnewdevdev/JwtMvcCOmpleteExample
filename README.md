# JWT MVC Complete Example

This repository contains a simple ASP.NET Core MVC sample that demonstrates **JWT token authentication** with three roles:

- **Admin**
- **User**
- **Staff**

The application includes the requested services:

- `AuthendicationService`
- `UserService`
- `ApiGateWay`

It also includes the requested MVC pages:

- `Login` page
- `AdminPage`
- `UserPage`
- `StaffPage`

## How it works

1. The `Login` page accepts one of the three sample users.
2. `AuthendicationService` validates credentials by using `UserService`.
3. When the credentials are valid, `AuthendicationService` creates a JWT token.
4. The JWT token is stored in an **HttpOnly cookie**.
5. ASP.NET Core JWT bearer authentication reads that token from the cookie for every request.
6. MVC actions use role-based authorization so each role can open only its own page.
7. `ApiGateWay` calls the protected `/api/gateway/me` endpoint to show authenticated user details on the home page.

## Sample users

| Role | Username | Password |
| --- | --- | --- |
| Admin | `admin` | `Admin@123` |
| User | `user` | `User@123` |
| Staff | `staff` | `Staff@123` |

## Project structure

- `Program.cs` - dependency injection, JWT authentication, authorization, and MVC pipeline.
- `Services/AuthendicationService.cs` - JWT token creation and cookie handling.
- `Services/UserService.cs` - in-memory sample users.
- `Services/ApiGateWay.cs` - protected API call from MVC to API.
- `Controllers/AccountController.cs` - login and logout.
- `Controllers/HomeController.cs` - home page and role pages.
- `Controllers/GatewayApiController.cs` - secure API endpoint used by `ApiGateWay`.
- `Views/` - MVC pages.

## Run the sample

### Prerequisites

Install the **.NET 8 SDK** or later.

### Commands

```bash
dotnet restore
dotnet run
```

Then open the browser to the URL shown in the terminal, usually:

- `https://localhost:7147`
- or `http://localhost:5147`

## Test the role behavior

1. Log in as `admin` and open **Admin page**.
2. Log out and log in as `user` to open **User page**.
3. Log out and log in as `staff` to open **Staff page**.
4. Try opening a page for a different role to verify the **Access Denied** flow.

## Notes

- This sample keeps users in memory for simplicity.
- For a real application, replace `UserService` with a database-backed or identity-provider-backed implementation.
- Replace the demo JWT secret in `appsettings.json` before using the code outside of a learning environment.
