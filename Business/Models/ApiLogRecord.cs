namespace Business.Models
{
    public class ApiLogRecord
    {
        public int ApiLogId { get; set; }
        public string ApiName { get; set; } = "";
        public string RequestUrl { get; set; } = "";
        public string? RequestPayload { get; set; }
        public string? ResponsePayload { get; set; }
        public int? StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ElapsedMs { get; set; }
    }
}
