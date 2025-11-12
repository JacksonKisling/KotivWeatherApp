namespace Business.DTOs
{
    public class ErrorLogDto
    {
        public int ErrorId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public string? ErrorSource { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
