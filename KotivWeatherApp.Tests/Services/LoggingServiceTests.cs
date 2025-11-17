using Business.Repositories;
using Business.Services;
using Moq;

namespace KotivWeatherApp.Tests.Services
{
    public class LoggingServiceTests
    {
        [Fact]
        public async Task LogErrorAsync_ShouldInsertErrorRecord()
        {
            // Arrange
            var mockErrorRepo = new Mock<IErrorLogRepository>();
            var mockApiRepo = new Mock<IApiLogRepository>();

            var service = new LoggingService(mockErrorRepo.Object, mockApiRepo.Object);

            var ex = new Exception("Test exception");

            // Act
            await service.LogErrorAsync(ex, "TestMethod");

            // Assert
            mockErrorRepo.Verify(r => r.InsertErrorAsync(It.IsAny<Business.DTOs.ErrorLogDto>()), Times.Once);
        }
    }
}
