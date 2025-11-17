using Business.Models;
using System.Text.Json;

namespace Business.Services
{
    public class GoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggingService _logger;
        private readonly string _apiKey;

        public GoogleMapsService(HttpClient httpClient, LoggingService logger, string apiKey)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = apiKey;
        }

        public async Task<GeoResult?> GetCoordinatesAsync(string searchTerm)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(searchTerm)}&key={_apiKey}";

            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                sw.Stop();

                await _logger.LogApiCallAsync("Google Maps", url, null, json, (int)response.StatusCode, (int)sw.ElapsedMilliseconds);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = JsonSerializer.Deserialize<GoogleGeocodeResponse>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result?.Results?.FirstOrDefault()?.Geometry?.Location;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, nameof(GetCoordinatesAsync));
                return null;
            }
        }

        public async Task<string?> ReverseGeocodeAsync(double lat, double lng)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={_apiKey}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var json = JsonDocument.Parse(response);

                var results = json.RootElement.GetProperty("results");

                if (results.GetArrayLength() == 0)
                    return null;

                return results[0].GetProperty("formatted_address").GetString();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex);
                return null;
            }
        }

    }
}
