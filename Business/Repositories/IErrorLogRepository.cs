using Business.DTOs;
using Business.Models;

namespace Business.Repositories
{
    public interface IErrorLogRepository
    {
        Task<int> InsertErrorAsync(ErrorLogDto dto);
        Task<IEnumerable<ErrorLogRecord>> GetAllAsync();
    }
}