using Business.DTOs;
using Business.Repositories;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SearchHistoryService
    {
        private readonly SearchHistoryRepository _repo;

        public SearchHistoryService(string connectionString)
        {
            _repo = new SearchHistoryRepository(connectionString);
        }

        public Task<int> AddSearchAsync(string searchTerm, double lat, double lon, string? summary)
        {
            var dto = new SearchHistoryDto
            {
                SearchTerm = searchTerm,
                Latitude = (decimal)lat,
                Longitude = (decimal)lon,
                ResultSummary = summary,
                SearchTimestamp = DateTime.UtcNow
            };

            return _repo.InsertSearchAsync(dto);
        }

        public Task<IEnumerable<SearchHistoryDto>> GetRecentSearchesAsync() => _repo.GetRecentSearchesAsync(5);
    }
}
