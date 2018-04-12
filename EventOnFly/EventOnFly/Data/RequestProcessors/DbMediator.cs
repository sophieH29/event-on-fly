using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventOnFly.Data.RequestProcessors
{
    public interface IDbMediator
    {
        Task<CommandResponse> ExecuteProcedure(ProcedureName procedureName, params object[] parameters);

        Task<int> ExecuteProcedureNonQuery(ProcedureName procedureName, params object[] parameters);
    }

    public class DbMediator: IDbMediator
    {
        private readonly string connectionString;

        public DbMediator(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MyDbConnection");

            SqlConnection conn = new SqlConnection(connectionString);
            var cm = new SqlCommand("", conn);
            var reder = cm.ExecuteReaderAsync();

        }

        public async Task<CommandResponse> ExecuteProcedure(ProcedureName procedureName, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> ExecuteProcedureNonQuery(ProcedureName procedureName, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
