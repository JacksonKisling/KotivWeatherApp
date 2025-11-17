using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Business.Repositories
{
    public abstract class RepositoryBase<T>
    {
        private readonly string? _connectionString;
        private readonly IDbConnection? _externalConnection;

        protected RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected RepositoryBase(IDbConnection connection)
        {
            _externalConnection = connection;
        }

        protected IDbConnection OpenConnection()
        {
            if (_externalConnection != null)
            {
                return _externalConnection;
            }

            if (_connectionString == null)
                throw new InvalidOperationException("No connection string provided.");

            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            try
            {
                using var conn = OpenConnection();
                return await conn.ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL ERROR in ExecuteAsync:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<T?> QuerySingleAsync(string sql, object? param = null)
        {
            try
            {
                using var conn = OpenConnection();
                return await conn.QuerySingleOrDefaultAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL ERROR in QuerySingleAsync:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object? param = null)
        {
            try
            {
                using var conn = OpenConnection();
                return await conn.QueryAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL ERROR in QueryAsync:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
