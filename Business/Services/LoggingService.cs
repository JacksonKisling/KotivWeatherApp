using System;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Repositories;

namespace Business.Services
{
    public class LoggingService
    {
        private readonly ErrorLogRepository _errorRepo;
        private readonly ApiLogRepository _apiRepo;

        public LoggingService(string connectionString)
        {
            _errorRepo = new ErrorLogRepository(connectionString);
            _apiRepo = new ApiLogRepository(connectionString);
        }

        public async Task LogErrorAsync(Exception ex, string? source = null)
        {
            var dto = new ErrorLogDto
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                ErrorSource = source ?? ex.TargetSite?.Name ?? "Unknown",
                Timestamp = DateTime.UtcNow
            };

            await _errorRepo.InsertErrorAsync(dto);
        }

        public async Task LogApiCallAsync(
            string apiName,
            string requestUrl,
            string? requestPayload,
            string? responsePayload,
            int? statusCode,
            int? elapsedMs)
        {
            var dto = new ApiLogDto
            {
                ApiName = apiName,
                RequestUrl = requestUrl,
                RequestPayload = requestPayload,
                ResponsePayload = responsePayload,
                StatusCode = statusCode,
                ElapsedMs = elapsedMs,
                Timestamp = DateTime.UtcNow
            };

            await _apiRepo.InsertLogAsync(dto);
        }
    }
}
