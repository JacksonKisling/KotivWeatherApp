using Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class ApiLogRepository : RepositoryBase<ApiLogDto>
    {
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

        public async Task<IEnumerable<ApiLogDto>> GetAllAsync()
        {
            const string sql = "SELECT * FROM [dbo].[ApiLog] ORDER BY [Timestamp] DESC;";
            return await QueryAsync(sql);
        }
    }
}
