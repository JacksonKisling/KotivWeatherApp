using Business.Models;
using System.Text.Json;

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

            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("KotivWeatherApp (contact@example.com)");

            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();

                var pointResponse = await _httpClient.GetAsync(pointUrl);
                var pointJson = await pointResponse.Content.ReadAsStringAsync();
                sw.Stop();

                await _logger.LogApiCallAsync("NWS Points", pointUrl, null, pointJson,
                    (int)pointResponse.StatusCode, (int)sw.ElapsedMilliseconds);

                if (!pointResponse.IsSuccessStatusCode)
                    return null;

                using var doc = JsonDocument.Parse(pointJson);

                if (!doc.RootElement.TryGetProperty("properties", out var props) ||
                    !props.TryGetProperty("forecast", out var forecastProp))
                {
                    return null;
                }

                var forecastUrl = forecastProp.GetString();

                if (string.IsNullOrWhiteSpace(forecastUrl))
                    return null;

                _httpClient.DefaultRequestHeaders.UserAgent.Clear();
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("KotivWeatherApp (contact@example.com)");

                sw.Restart();
                var forecastResponse = await _httpClient.GetAsync(forecastUrl);
                var forecastJson = await forecastResponse.Content.ReadAsStringAsync();
                sw.Stop();

                await _logger.LogApiCallAsync("NWS Forecast", forecastUrl, null, forecastJson,
                    (int)forecastResponse.StatusCode, (int)sw.ElapsedMilliseconds);

                if (!forecastResponse.IsSuccessStatusCode)
                    return null;

                var forecast = JsonSerializer.Deserialize<WeatherForecastResponse>(forecastJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
