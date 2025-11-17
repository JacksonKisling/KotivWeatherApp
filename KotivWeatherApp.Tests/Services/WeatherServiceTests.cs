using Business.Repositories;
using Business.Services;
using FluentAssertions;
using KotivWeatherApp.Tests.TestHelpers;
using Moq;
using System.Net;

namespace KotivWeatherApp.Tests.Services
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetForecastAsync_ShouldReturnWeatherData_WhenApiSucceeds()
        {
            // Arrange
            var fakePointsJson = """{ "properties": { "forecast": "https://fake.nws/forecast" } }""";
            var fakeForecastJson = """
    {
        "properties": {
            "periods": [
                { "name": "Today", "temperature": 72, "temperatureUnit": "F", "shortForecast": "Sunny" }
            ]
        }
    }
    """;

            var fakePointsResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakePointsJson)
            };
            var fakeForecastResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakeForecastJson)
            };

            var handler = new FakeHttpMessageHandler();
            handler.HandlerFunc = (req, ct) =>
            {
                if (req.RequestUri!.AbsoluteUri.Contains("forecast"))
                    return Task.FromResult(fakeForecastResponse);
                return Task.FromResult(fakePointsResponse);
            };

            var client = new HttpClient(handler);

            var mockErrorRepo = new Mock<IErrorLogRepository>();
            var mockApiRepo = new Mock<IApiLogRepository>();
            var logger = new LoggingService(mockErrorRepo.Object, mockApiRepo.Object);

            var service = new WeatherService(client, logger);

            // Act
            var result = await service.GetForecastAsync(39.74, -104.99);

            // Assert
            result.Should().NotBeNull();
            result!.Properties!.Periods.Should().HaveCount(1);
            result.Properties.Periods[0].ShortForecast.Should().Be("Sunny");
        }

    }
}
