// <copyright file="WeightLogTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.Tests.Entities;

using Portal.Domain.Entities;

/// <summary>
/// Tests for the <see cref="WeightLog"/> entity.
/// </summary>
public class WeightLogTests
{
    [Fact]
    public void Constructor_WhenCalled_SetsPropertiesCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var weight = new Domain.ValueObjects.Weight(70.5m);
        var userId = Guid.NewGuid();
        var id = Guid.NewGuid();
        const string notes = "Morning weigh-in";

        // Act
        var weightLog = new WeightLog(date, weight, userId, notes, id) { User = new("test") };

        // Assert
        weightLog.Date.ShouldBe(date);
        weightLog.Weight.ShouldBe(weight);
        weightLog.UserId.ShouldBe(userId);
        weightLog.User.Subject.ShouldBe("test");
        weightLog.Notes.ShouldBe(notes);
        weightLog.Id.ShouldBe(id);
    }

    [Fact]
    public void Constructor_NoId_SetsProperty()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var weight = new Domain.ValueObjects.Weight(70.5m);

        // Act
        var weightLog = new WeightLog(date, weight, Guid.Empty);

        // Assert
        weightLog.Id.ShouldNotBe(Guid.Empty);
    }
}
