namespace Business.DTOs
{
    public class ApiLogDto
    {
        public int ApiLogId { get; set; }
        public string ApiName { get; set; } = string.Empty;
        public string RequestUrl { get; set; } = string.Empty;
        public string? RequestPayload { get; set; }
        public string? ResponsePayload { get; set; }
        public int? StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ElapsedMs { get; set; }
    }
}
