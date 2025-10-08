// <copyright file="ForecastServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Tests.Weather;

using Portal.Application.Weather;
using Portal.Domain.Weather;

public class ForecastServiceTests
{
    [Fact]
    public void GetForecast_WhenCalled_ReturnsExpected()
    {
        // Arrange
        var mockRepo = new Mock<IForecastRepo>();
        var sut = new ForecastService(mockRepo.Object);
        _ = mockRepo.Setup(m => m.GetForecast())
            .Returns(Enumerable.Range(1, 3).Select(index => new Forecast
            {
                Date = DateOnly.MinValue,
                TemperatureC = 25,
                Summary = "Freezing",
            }));

        // Act
        var actual = sut.GetForecast().ToList();

        // Assert
        actual.Count.ShouldBe(3);
        actual[0].TemperatureF.ShouldBe(77);

    }
}