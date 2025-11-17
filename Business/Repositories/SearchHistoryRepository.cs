using Business.DTOs;
using System.Data;

namespace Business.Repositories
{
    public class SearchHistoryRepository : RepositoryBase<SearchHistoryDto>, ISearchHistoryRepository
    {
        public SearchHistoryRepository(IDbConnection connection) : base(connection) { }

        public SearchHistoryRepository(string connectionString)  : base(connectionString) { }

        public async Task<IEnumerable<SearchHistoryDto>> GetRecentSearchesAsync(int count = 5)
        {
            const string sql = @"SELECT TOP (@Count) * 
                                 FROM [dbo].[SearchHistory] 
                                 ORDER BY [SearchTimestamp] DESC;";
            return await QueryAsync(sql, new { Count = count });
        }

        public async Task<int> InsertSearchAsync(SearchHistoryDto dto)
        {
            const string sql = @"
                INSERT INTO [dbo].[SearchHistory] (SearchTerm, Latitude, Longitude, ResultSummary, SearchTimestamp)
                VALUES (@SearchTerm, @Latitude, @Longitude, @ResultSummary, @SearchTimestamp);";
            return await ExecuteAsync(sql, dto);
        }

        public async Task<IEnumerable<SearchHistoryDto>> GetLastFiveAsync()
        {
            var sql = @"
                SELECT TOP 5 Id, SearchTerm, Latitude, Longitude, SearchTimestamp
                FROM SearchHistory
                ORDER BY SearchTimestamp DESC";
            return await QueryAsync(sql);
        }

    }
}
