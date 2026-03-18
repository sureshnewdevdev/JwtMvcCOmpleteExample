using System.Net.Http.Headers;
using System.Text;
using JwtMvcCOmpleteExample.Models;
using JwtMvcCOmpleteExample.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Register MVC with views so the sample can serve the login page and role pages.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Configure strongly typed JWT settings from appsettings.json.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
    ?? throw new InvalidOperationException("JWT configuration is missing.");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

// Register the requested services used throughout the sample.
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<AuthendicationService>();
builder.Services.AddScoped<ApiGateWay>();

// ApiGateWay uses HttpClient to demonstrate how an MVC app can call a JWT-protected API.
builder.Services.AddHttpClient<ApiGateWay>((serviceProvider, client) =>
{
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var request = httpContextAccessor.HttpContext?.Request;

    if (request is not null)
    {
        client.BaseAddress = new Uri($"{request.Scheme}://{request.Host}");
    }

    var token = httpContextAccessor.HttpContext?.Request.Cookies[AuthendicationService.TokenCookieName];
    if (!string.IsNullOrWhiteSpace(token))
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
});

// Configure JWT bearer authentication and read the token from an HttpOnly cookie.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue(AuthendicationService.TokenCookieName, out var token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                // Redirect browser requests to the login page instead of returning a blank 401 response.
                if (!context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/api"))
                {
                    context.HandleResponse();
                    context.Response.Redirect("/Account/Login");
                }

                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                if (!context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.Redirect("/Home/AccessDenied");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
