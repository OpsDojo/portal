// <copyright file="InMemForecastRepoTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.Tests.Repositories;

/// <summary>
/// Tests for the <see cref="InMemForecastRepo"/> class.
/// </summary>
public class InMemForecastRepoTests
{
    [Fact]
    public void GetForecast_WhenCalled_ReturnsExpected()
    {
        // Arrange
        var sut = new InMemForecastRepo();

        // Act
        var forecast = sut.GetForecast();

        // Assert
        forecast.ShouldNotBeNull();
        forecast.Count.ShouldBe(5);
    }
}
