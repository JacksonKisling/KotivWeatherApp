namespace Business.Services.Models
{
    public class GeoResult
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class GoogleGeocodeResponse
    {
        public List<Result>? Results { get; set; }
    }

    public class Result
    {
        public Geometry? Geometry { get; set; }
    }

    public class Geometry
    {
        public GeoResult? Location { get; set; }
    }
}
