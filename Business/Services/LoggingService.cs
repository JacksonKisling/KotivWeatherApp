using Business.Repositories;
using Business.DTOs;

namespace Business.Services
{
    public class LoggingService
    {
        private readonly IErrorLogRepository _errorRepo;
        private readonly IApiLogRepository _apiRepo;

        public LoggingService(string connectionString)
        {
            _errorRepo = new ErrorLogRepository(connectionString);
            _apiRepo = new ApiLogRepository(connectionString);
        }

        public LoggingService(IErrorLogRepository errorRepo, IApiLogRepository apiRepo)
        {
            _errorRepo = errorRepo;
            _apiRepo = apiRepo;
        }

        public Task LogErrorAsync(Exception ex, string? source = null) =>
            _errorRepo.InsertErrorAsync(new ErrorLogDto
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                ErrorSource = source ?? ex.TargetSite?.Name ?? "Unknown",
                Timestamp = DateTime.UtcNow
            });

        public Task LogApiCallAsync(
            string apiName, string requestUrl,
            string? requestPayload, string? responsePayload,
            int? statusCode, int? elapsedMs) =>
            _apiRepo.InsertLogAsync(new ApiLogDto
            {
                ApiName = apiName,
                RequestUrl = requestUrl,
                RequestPayload = requestPayload,
                ResponsePayload = responsePayload,
                StatusCode = statusCode,
                ElapsedMs = elapsedMs,
                Timestamp = DateTime.UtcNow
            });
    }
}
