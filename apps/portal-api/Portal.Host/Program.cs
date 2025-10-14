// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using Portal.Host.Startup;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddCorsSupport(config);
builder.Services.AddSwaggerSupport(xmlFilePath: SwaggerExtensions.GetXmlFilePath<Program>());
builder.Services.AddWeatherFeature();
builder.Services.AddHealthChecks();
builder.Services.AddAuthSupport(config);

var app = builder.Build();
app.UseCorsSupport();
app.UseSwaggerSupport();
app.UseHttpsRedirection();
app.UseAuthSupport();
app.MapHealthChecks("/healthz");

await app.RunAsync();
