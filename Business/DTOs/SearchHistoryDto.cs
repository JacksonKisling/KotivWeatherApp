namespace Business.DTOs
{
    public class SearchHistoryDto
    {
        public int Id { get; set; }

        public string SearchTerm { get; set; } = "";

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime SearchTimestamp { get; set; }

        public string? ResultSummary { get; set; }
    }
}
