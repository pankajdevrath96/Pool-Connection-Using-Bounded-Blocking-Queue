using Microsoft.Data.SqlClient;
using System.Collections.Concurrent;

namespace Pool_Connection_Using_Bounded_Blocking_Queue
{
    public class DB : IDisposable
    {

        private readonly BlockingCollection<SqlConnection> _pool;
        private readonly string _connectionString;
        private readonly int _maxPoolSize;

        public DB(string connectionString, int maxPoolSize)
        {
            _connectionString = connectionString;
            _maxPoolSize = maxPoolSize;
            _pool = new BlockingCollection<SqlConnection>(boundedCapacity: maxPoolSize);

            for (int i = 0; i < _maxPoolSize; i++)
            {
                _pool.Add(CreateConnection());
            }
        }

        private SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public SqlConnection AquireConnection()
        {
            try
            {
                return _pool.Take();
            }
            catch (InvalidOperationException)
            {
                throw new Exception("No available connections in the pool.");
            }
        }

        public void ReleaseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                _pool.Add(connection);
            }
        }

        public void Dispose()
        {
            while (_pool.TryTake(out SqlConnection conn))
            {
                conn.Dispose();
            }
            _pool.Dispose();
        }
    }
}
