namespace Business.Models
{
    public class SearchHistoryRecord
    {
        public int Id { get; set; }
        public string SearchTerm { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime SearchTimestamp { get; set; }
        public string? ResultSummary { get; set; }
    }
}
