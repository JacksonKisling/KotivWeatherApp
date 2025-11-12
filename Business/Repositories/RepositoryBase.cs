using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class RepositoryBase<T> where T : class, new()
    {
        protected readonly string _connectionString;

        public RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected async Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null)
        {
            using var db = new DataAccess.DatabaseContext(_connectionString);
            return await db.Connection.QueryAsync<T>(sql, parameters);
        }

        protected async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            using var db = new DataAccess.DatabaseContext(_connectionString);
            return await db.Connection.ExecuteAsync(sql, parameters);
        }

        protected async Task<T?> QuerySingleAsync(string sql, object? parameters = null)
        {
            using var db = new DataAccess.DatabaseContext(_connectionString);
            return await db.Connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
    }
}
