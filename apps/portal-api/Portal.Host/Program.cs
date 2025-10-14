// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using Portal.Application.Weather;
using Portal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddDefaultPolicy(policy =>
{
    var origins = config.GetSection("Cors:Origins").Get<string[]>()!;
    _ = policy.WithOrigins(origins)
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IForecastRepo, InMemForecastRepo>();
builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");

await app.RunAsync();
