using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Business.Services.Models;

namespace Business.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggingService _logger;

        public WeatherService(HttpClient httpClient, LoggingService logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<WeatherForecastResponse?> GetForecastAsync(double lat, double lon)
        {
            var pointUrl = $"https://api.weather.gov/points/{lat},{lon}";

            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                var pointResponse = await _httpClient.GetAsync(pointUrl);
                var pointJson = await pointResponse.Content.ReadAsStringAsync();
                sw.Stop();

                await _logger.LogApiCallAsync("NWS Points", pointUrl, null, pointJson, (int)pointResponse.StatusCode, (int)sw.ElapsedMilliseconds);

                if (!pointResponse.IsSuccessStatusCode)
                    return null;

                using var doc = JsonDocument.Parse(pointJson);
                var forecastUrl = doc.RootElement.GetProperty("properties").GetProperty("forecast").GetString();

                if (forecastUrl == null)
                    return null;

                sw.Restart();
                var forecastResponse = await _httpClient.GetAsync(forecastUrl);
                var forecastJson = await forecastResponse.Content.ReadAsStringAsync();
                sw.Stop();

                await _logger.LogApiCallAsync("NWS Forecast", forecastUrl, null, forecastJson, (int)forecastResponse.StatusCode, (int)sw.ElapsedMilliseconds);

                if (!forecastResponse.IsSuccessStatusCode)
                    return null;

                var forecast = JsonSerializer.Deserialize<WeatherForecastResponse>(forecastJson);
                return forecast;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, nameof(GetForecastAsync));
                return null;
            }
        }
    }
}
