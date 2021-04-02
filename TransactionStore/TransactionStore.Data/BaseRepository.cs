using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TransactionStore.Core.Settings;

namespace TransactionStore.Data
{
    public abstract class BaseRepository
    {
        protected SqlConnection _connection;
        protected string _connectionString;
        public BaseRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.CONNECTION_STRING;
        }
    }
}