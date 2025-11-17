using Business.DTOs;
using Business.Repositories;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services
{
    public class SearchHistoryService
    {
        private readonly ISearchHistoryRepository _repository;

        public SearchHistoryService(string connectionString)
        {
            _repository = new SearchHistoryRepository(connectionString);
        }

        public SearchHistoryService(ISearchHistoryRepository repo)
        {
            _repository = repo;
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

            return _repository.InsertSearchAsync(dto);
        }

        public Task<IEnumerable<SearchHistoryDto>> GetRecentSearchesAsync() => _repository.GetRecentSearchesAsync(5);

        public async Task<IEnumerable<SearchHistoryRecord>> GetLastFiveAsync()
        {
            var dtos = await _repository.GetLastFiveAsync();

            return dtos.Select(d => new SearchHistoryRecord
            {
                Id = d.Id,
                SearchTerm = d.SearchTerm,
                Latitude = (double)d.Latitude,
                Longitude = (double)d.Longitude,
                SearchTimestamp = d.SearchTimestamp,
                ResultSummary = d.ResultSummary
            });
        }

    }
}
