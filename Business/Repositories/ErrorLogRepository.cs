using Business.DTOs;
using Business.Models;
using System.Data;

namespace Business.Repositories
{
    public class ErrorLogRepository : RepositoryBase<ErrorLogDto>, IErrorLogRepository
    {
        public ErrorLogRepository(string connectionString) : base(connectionString) { }

        public ErrorLogRepository(IDbConnection connection) : base(connection) { }

        public async Task<int> InsertErrorAsync(ErrorLogDto dto)
        {
            const string sql = @"
                INSERT INTO [dbo].[ErrorLog] 
                    (ErrorMessage, StackTrace, ErrorSource)
                VALUES 
                    (@ErrorMessage, @StackTrace, @ErrorSource);";
            return await ExecuteAsync(sql, dto);
        }

        public async Task<IEnumerable<ErrorLogRecord>> GetAllAsync()
        {
            const string sql = @"
                SELECT ErrorId,
                       ErrorMessage,
                       StackTrace,
                       ErrorSource,
                       Timestamp
                FROM ErrorLog
                ORDER BY Timestamp DESC;
            ";

            var dtos = await QueryAsync(sql);

            return dtos.Select(d => new ErrorLogRecord
            {
                ErrorId = d.ErrorId,
                ErrorMessage = d.ErrorMessage,
                StackTrace = d.StackTrace,
                ErrorSource = d.ErrorSource,
                Timestamp = d.Timestamp
            });
        }
    }
}
