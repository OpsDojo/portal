// <copyright file="UserTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.Tests.Entities;

using Portal.Domain.Entities;

/// <summary>
/// Tests for the <see cref="User"/> entity.
/// </summary>
public class UserTests
{
    [Fact]
    public void Constructor_WhenCalled_SetsProperties()
    {
        // Arrange
        const string subject = "Test Subject";
        const string displayName = "Test User";
        var expectedId = Guid.NewGuid();
        var date = DateTimeOffset.UtcNow;

        // Act
        var user = new User(subject, displayName, expectedId, date);

        // Assert
        user.Subject.ShouldBe(subject);
        user.DisplayName.ShouldBe(displayName);
        user.Id.ShouldBe(expectedId);
        user.Joined.ShouldBe(date);
    }

    [Fact]
    public void Constructor_MissingSubject_ThrowsExpected()
    {
        // Arrange
        const string subject = "";

        // Act
        var act = () => new User(subject);

        // Assert
        act.ShouldThrow<ArgumentException>(nameof(subject));
    }

    [Fact]
    public void ChangeDisplayName_WhenCalled_UpdatesDisplayName()
    {
        // Arrange
        const string subject = "Test Subject";
        const string initialDisplayName = "Initial Name";
        const string newDisplayName = "New Name";
        var user = new User(subject, initialDisplayName);

        // Act
        var updatedUser = user.ChangeDisplayName(newDisplayName);

        // Assert
        updatedUser.DisplayName.ShouldBe(newDisplayName);
    }
}
