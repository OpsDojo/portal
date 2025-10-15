// <copyright file="SwaggerAuthFilter.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Startup;

using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Operation filter that adds 401 and 403 data to operations that are not marked AllowAnonymous.
/// </summary>
public class SwaggerAuthFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        if (!IsAllowAnonymous(context.MethodInfo) && !IsAllowAnonymous(context.MethodInfo.DeclaringType!))
        {
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
        }
    }

    private static bool IsAllowAnonymous(MemberInfo member) => member
        .GetCustomAttributes(true)
        .Any(attr => attr is AllowAnonymousAttribute);
}