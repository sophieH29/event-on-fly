using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventOnFly.Data.DbAccess
{
    public interface IDbMediator
    {
        Task<CommandResponse> ExecuteProcedure(ProcedureName procedureName, params ProcedureParameter[] parameters);

        Task<int> ExecuteProcedureNonQuery(ProcedureName procedureName, params ProcedureParameter[] parameters);
    }

    public class DbMediator: IDbMediator
    {
        private readonly string connectionString;

        public DbMediator(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MyDbConnection");
        }

        private async Task<SqlConnection> OpenDbConnection()
        {
            var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            return conn;
        }

        private string GetProcedureName(ProcedureName procedureName)
        {
            return $"usp{procedureName}";
        }

        public async Task<CommandResponse> ExecuteProcedure(ProcedureName procedureName, params ProcedureParameter[] parameters)
        {
            using (var conn = await OpenDbConnection())
            {
                var command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedureName(procedureName);
                await AddParamsToCommand(command, procedureName, parameters);
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
        }

        public async Task<int> ExecuteProcedureNonQuery(ProcedureName procedureName, params ProcedureParameter[] parameters)
        {
            using (var conn = await OpenDbConnection())
            {
                var command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedureName(procedureName);
                await AddParamsToCommand(command, procedureName, parameters);
                return await command.ExecuteNonQueryAsync();
            }            
        }

        private async Task AddParamsToCommand(
            SqlCommand command, ProcedureName procedureName, params ProcedureParameter[] parameters)
        {
            //TO DO: move to procedure
            var procName = GetProcedureName(procedureName);
            var script = $"select PARAMETER_NAME, DATA_TYPE from information_schema.parameters where specific_name = '{procName}'";
            var getParamsCommand = new SqlCommand(script, command.Connection);
            var dbParams = await getParamsCommand.ExecuteReaderAsync();
            var paramsDict = parameters.ToDictionary(p => p.ParameterName, p => p.Object);
            while(dbParams.Read())
            {
                var paramName = dbParams[0].ToString();
                var paramType = (SqlDbType)Enum.Parse(typeof(SqlDbType), dbParams[1].ToString());
                command.Parameters.Add(paramName, paramType).Value = paramsDict[paramName];        
            }
        }
    }
}
