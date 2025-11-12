namespace Business.DTOs
{
    public class SearchHistoryDto
    {
        public int SearchId { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime SearchTimestamp { get; set; }
        public string? ResultSummary { get; set; }
    }
}
