using Business.DTOs;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class SearchHistoryRepository : RepositoryBase<SearchHistoryDto>
    {
        public SearchHistoryRepository(string connectionString) : base(connectionString) { }

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
                INSERT INTO [dbo].[SearchHistory] (SearchTerm, Latitude, Longitude, ResultSummary)
                VALUES (@SearchTerm, @Latitude, @Longitude, @ResultSummary);";
            return await ExecuteAsync(sql, dto);
        }
    }
}
