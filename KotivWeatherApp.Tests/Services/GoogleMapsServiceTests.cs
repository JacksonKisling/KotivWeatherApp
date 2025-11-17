using Business.Repositories;
using Business.Services;
using FluentAssertions;
using KotivWeatherApp.Tests.TestHelpers;
using Moq;
using System.Net;

namespace KotivWeatherApp.Tests.Services
{
    public class GoogleMapsServiceTests
    {
        [Fact]
        public async Task GetCoordinatesAsync_ShouldReturnLocation_WhenApiReturnsValidJson()
        {
            // Arrange
            var fakeJson = """
            {
                "results": [
                    { "geometry": { "location": { "lat": 39.74, "lng": -104.99 } } }
                ]
            }
            """;

            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakeJson)
            };

            var handler = new FakeHttpMessageHandler
            {
                HandlerFunc = (req, ct) => Task.FromResult(fakeResponse)
            };

            var client = new HttpClient(handler);

            var mockErrorRepo = new Mock<IErrorLogRepository>();
            var mockApiRepo = new Mock<IApiLogRepository>();
            var logger = new LoggingService(mockErrorRepo.Object, mockApiRepo.Object);

            var service = new GoogleMapsService(client, logger, "fake-api-key");

            // Act
            var result = await service.GetCoordinatesAsync("Denver, CO");

            // Assert
            result.Should().NotBeNull();
            result!.Lat.Should().BeApproximately(39.74, 0.001);
            result.Lng.Should().BeApproximately(-104.99, 0.001);
        }
    }
}
