using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Business.Services.Models;

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

                var result = JsonSerializer.Deserialize<GoogleGeocodeResponse>(json);
                return result?.Results?.FirstOrDefault()?.Geometry?.Location;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, nameof(GetCoordinatesAsync));
                return null;
            }
        }
    }
}
