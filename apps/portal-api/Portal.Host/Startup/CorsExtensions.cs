// <copyright file="CorsExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Startup;

/// <summary>
/// Extensions for adding CORS support.
/// </summary>
public static class CorsExtensions
{
    /// <summary>
    /// Adds CORS support to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The application configuration.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IServiceCollection AddCorsSupport(this IServiceCollection services, IConfiguration config)
    {
        _ = services.AddCors(o => o.AddDefaultPolicy(policy =>
        {
            var origins = config.GetSection("Cors:Origins").Get<string[]>()!;
            _ = policy
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(origins)
                .WithExposedHeaders("Content-Disposition");
        }));

        return services;
    }

    /// <summary>
    /// Uses CORS support.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IApplicationBuilder UseCorsSupport(this IApplicationBuilder app) => app.UseCors();
}
