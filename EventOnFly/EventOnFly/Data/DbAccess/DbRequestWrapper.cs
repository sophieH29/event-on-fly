using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventOnFly.Data.DbAccess
{
    public interface IDbRequestWrapper
    {
        void ExecuteDbRequest(Func<DbRequestToken, Task> action);

        Task<T> ExecuteDbRequestWithResult<T>(Func<DbRequestToken,Task<T>> action);
    }

    public class DbRequestWrapper : IDbRequestWrapper
    {
        private readonly string connectionString;

        public DbRequestWrapper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MyDbConnection");
        }

        private async Task<DbRequestToken> OpenDbConnection()
        {
            var connection = new SqlConnection(connectionString);
            var transaction = connection.BeginTransaction();
            await connection.OpenAsync();
            return new DbRequestToken(connection, transaction);
        }

        public async void ExecuteDbRequest(Func<DbRequestToken, Task> action)
        {
            var token = await OpenDbConnection();
            try
            {
                await action(token);
                token.Commit();
            }
            finally
            {
                token.Dispose();
            }
        }

        public async Task<T> ExecuteDbRequestWithResult<T>(Func<DbRequestToken, Task<T>> action)
        {
            var token = await OpenDbConnection();
            try
            {
                var result = await action(token);
                token.Commit();
                return result;
            }
            finally
            {
                token.Dispose();
            }
        }
    }
}
