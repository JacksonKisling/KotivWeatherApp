using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Business.DataAccess
{
    public class DatabaseContext : IDisposable
    {
        private readonly IDbConnection _connection;
        private bool _disposed;

        public IDbConnection Connection => _connection;

        public DatabaseContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _connection?.Dispose();
                _disposed = true;
            }
        }
    }
}
