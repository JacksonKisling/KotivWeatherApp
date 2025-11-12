using Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class ErrorLogRepository : RepositoryBase<ErrorLogDto>
    {
        public ErrorLogRepository(string connectionString) : base(connectionString) { }

        public async Task<int> InsertErrorAsync(ErrorLogDto dto)
        {
            const string sql = @"
                INSERT INTO [dbo].[ErrorLog] 
                    (ErrorMessage, StackTrace, ErrorSource)
                VALUES 
                    (@ErrorMessage, @StackTrace, @ErrorSource);";
            return await ExecuteAsync(sql, dto);
        }

        public async Task<IEnumerable<ErrorLogDto>> GetAllAsync()
        {
            const string sql = "SELECT * FROM [dbo].[ErrorLog] ORDER BY [Timestamp] DESC;";
            return await QueryAsync(sql);
        }
    }
}
