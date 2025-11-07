// <copyright file="WeightLogServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Tests.Weight;

using Portal.Application.Weight;

/// <summary>
/// Tests for the <see cref="WeightLogService"/> class.
/// </summary>
public class WeightLogServiceTests
{
    [Fact]
    public async Task GetLogs_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IWeightLogRepository>();
        var sut = new WeightLogService(mockRepo.Object);
        var ct = CancellationToken.None;

        // Act
        _ = await sut.GetLogsAsync(Guid.NewGuid(), 1, 10, ct);

        // Assert
        mockRepo.Verify(
            x => x.GetPageAsync(It.IsAny<Guid>(), 1, 10, ct),
            Times.Once);
    }
}
