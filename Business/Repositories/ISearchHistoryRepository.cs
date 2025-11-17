using Business.DTOs;

public interface ISearchHistoryRepository
{
    Task<int> InsertSearchAsync(SearchHistoryDto dto);
    Task<IEnumerable<SearchHistoryDto>> GetRecentSearchesAsync(int count = 5);
    Task<IEnumerable<SearchHistoryDto>> GetLastFiveAsync();

}
