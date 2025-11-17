using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace KotivWeatherApp.Mvc.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weather;
        private readonly GoogleMapsService _google;
        private readonly SearchHistoryService _history;

        public WeatherController(
            WeatherService weather,
            GoogleMapsService google,
            SearchHistoryService history)
        {
            _weather = weather;
            _google = google;
            _history = history;
        }

        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Content("<p>Please enter a location.</p>", "text/html");

            var coords = await _google.GetCoordinatesAsync(term);

            if (coords == null)
                return Content("<p>Location not found.</p>", "text/html");

            double lat = coords.Lat;
            double lng = coords.Lng;

            var forecast = await _weather.GetForecastAsync(lat, lng);

            if (forecast != null)
            {
                forecast.Latitude = (decimal)lat;
                forecast.Longitude = (decimal)lng;
            }

            var placeName = await _google.ReverseGeocodeAsync(lat, lng) ?? term;

            await _history.AddSearchAsync(
                placeName,
                lat,
                lng,
                forecast?.Properties?.Periods?.FirstOrDefault()?.ShortForecast
            );

            ViewBag.LocationName = placeName;

            return PartialView("_ForecastPartial", forecast);
        }


        public async Task<IActionResult> FromCoords(double lat, double lng)
        {
            var forecast = await _weather.GetForecastAsync(lat, lng);

            if (forecast != null)
            {
                forecast.Latitude = (decimal)lat;
                forecast.Longitude = (decimal)lng;
            }

            var placeName = await _google.ReverseGeocodeAsync(lat, lng) ?? $"({lat:F4}, {lng:F4})";

            await _history.AddSearchAsync(placeName, lat, lng, forecast?.Properties?.Periods?.FirstOrDefault()?.ShortForecast);

            ViewBag.LocationName = placeName;
            return PartialView("_ForecastPartial", forecast);
        }

    }
}
