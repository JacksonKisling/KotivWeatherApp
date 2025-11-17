using Business.DTOs;
using Business.Models;
using System.Data;

namespace Business.Repositories
{
    public class ApiLogRepository : RepositoryBase<ApiLogDto>, IApiLogRepository
    {
        public ApiLogRepository(IDbConnection connection) : base(connection) { }

        public ApiLogRepository(string connectionString) : base(connectionString) { }

        public async Task<int> InsertLogAsync(ApiLogDto dto)
        {
            const string sql = @"
                INSERT INTO [dbo].[ApiLog] 
                    (ApiName, RequestUrl, RequestPayload, ResponsePayload, StatusCode, ElapsedMs)
                VALUES 
                    (@ApiName, @RequestUrl, @RequestPayload, @ResponsePayload, @StatusCode, @ElapsedMs);";
            return await ExecuteAsync(sql, dto);
        }

        public async Task<IEnumerable<ApiLogRecord>> GetAllAsync()
        {
            const string sql = @"
                SELECT ApiLogId, ApiName, RequestUrl, RequestPayload, ResponsePayload,
                       StatusCode, Timestamp, ElapsedMs
                FROM ApiLog
                ORDER BY Timestamp DESC;
            ";

            var dtos = await QueryAsync(sql);

            return dtos.Select(d => new ApiLogRecord
            {
                ApiLogId = d.ApiLogId,
                ApiName = d.ApiName,
                RequestUrl = d.RequestUrl,
                RequestPayload = d.RequestPayload,
                ResponsePayload = d.ResponsePayload,
                StatusCode = d.StatusCode,
                Timestamp = d.Timestamp,
                ElapsedMs = d.ElapsedMs
            });
        }

    }
}
