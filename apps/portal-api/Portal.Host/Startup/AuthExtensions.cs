// <copyright file="AuthExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Startup;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

/// <summary>
/// Extensions for authentication setup.
/// </summary>
public static class AuthExtensions
{
    /// <summary>
    /// Adds authentication support to the service collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="config">The configuration.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IServiceCollection AddAuthSupport(this IServiceCollection services, IConfiguration config)
    {
        _ = services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(
                opts =>
                {
                    opts.TokenValidationParameters.NameClaimType = ClaimTypes.NameIdentifier;
                    opts.TokenValidationParameters.RoleClaimType = "http://schemas.microsoft.com/identity/claims/scope";
                    opts.Events = new();
                    opts.Events.OnMessageReceived += (ctx) =>
                    {
                        ctx.Token = ctx.Request.Query.TryGetValue("access_token", out var t) ? (string?)t : null;
                        return Task.CompletedTask;
                    };
                },
                identityOpts => config.Bind("AzureAd", identityOpts));

        return services;
    }

    /// <summary>
    /// Uses authentication and authorization middleware.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IApplicationBuilder UseAuthSupport(this IApplicationBuilder app)
    {
        _ = app.UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

        return app;
    }
}
