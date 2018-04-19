using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EventOnFly.Data.DbAccess.Parameters;

namespace EventOnFly.Data.DbAccess
{
    public interface IDbMediator
    {
        Task<CommandResponse> ExecuteProcedure(ProcedureName procedureName, DbRequestToken token, params DataBaseParameter[] parameters);

        Task<int> ExecuteProcedureNonQuery(ProcedureName procedureName, DbRequestToken token, params DataBaseParameter[] parameters);
    }

    public class DbMediator : IDbMediator
    {
        public async Task<CommandResponse> ExecuteProcedure(ProcedureName procedureName, DbRequestToken token, params DataBaseParameter[] parameters)
        {
            var command = token.Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName.GetStringName();
            command.Transaction = token.Transaction;
            AddParamsToCommand(command, parameters);
            var reader = await command.ExecuteReaderAsync();
            var resp = new CommandResponse();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                resp.ColumnNames.Add(reader.GetName(i));
            }
            while (reader.Read())
            {
                var currentVals = new List<object>(reader.FieldCount);
                for (var i = 0; i < reader.FieldCount; ++i)
                    currentVals.Add(reader[i]);
            }
            return resp;
        }

        public async Task<int> ExecuteProcedureNonQuery(ProcedureName procedureName, DbRequestToken token, params DataBaseParameter[] parameters)
        {
            var command = token.Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName.GetStringName();
            command.Transaction = token.Transaction;
            AddParamsToCommand(command, parameters);
            return await command.ExecuteNonQueryAsync();
        }

        private void AddParamsToCommand(SqlCommand command, params DataBaseParameter[] parameters)
        {
            foreach (var param in parameters)
            {
                command.Parameters.Add(param.ParameterName, param.DbType).Value = param.Object;
            }
        }
    }
}
