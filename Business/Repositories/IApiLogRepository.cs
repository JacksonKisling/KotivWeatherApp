using Business.DTOs;
using Business.Models;

namespace Business.Repositories
{
    public interface IApiLogRepository
    {
        Task<int> InsertLogAsync(ApiLogDto dto);
        Task<IEnumerable<ApiLogRecord>> GetAllAsync();
    }
}