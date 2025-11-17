namespace Business.Models
{
    public class WeatherForecastResponse
    {
        public Properties? Properties { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class Properties
    {
        public List<Period>? Periods { get; set; }
    }

    public class Period
    {
        public string? Name { get; set; }
        public string? DetailedForecast { get; set; }
        public string? TemperatureUnit { get; set; }
        public double Temperature { get; set; }
        public string? ShortForecast { get; set; }
        public string? Icon { get; set; }

    }
}
