using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EventOnFly.Data.DbAccess.Parameters;

namespace EventOnFly.Data.DbAccess
{
    public interface IProcedureExecutor
    {
        Task<IEnumerable<T>> ExecProcedure<T>(ProcedureName procedureName, params ProcedureParameter[] parameters) where T : new();

        Task<T> ExecProcedureNonQuery<T>(ProcedureName procedureName, params ProcedureParameter[] parameters) where T : struct;

        Task ExecuteProcedureNoResult(ProcedureName procedureName, params ProcedureParameter[] parameters);
    }

    public class ProcedureExecutor : IProcedureExecutor
    {
        private readonly IDbMediator dbMediator;

        public ProcedureExecutor(IDbMediator dbMediator)
        {
            this.dbMediator = dbMediator;
        }

        public async Task<IEnumerable<T>> ExecProcedure<T>(ProcedureName procedureName, params ProcedureParameter[] parameters) where T : new()
        {
            var dbParams = await GetDbParameters(procedureName, parameters);
            var result = await dbMediator.ExecuteProcedure(procedureName, dbParams);
            return result.MapToType<T>();
        }

        public async Task<T> ExecProcedureNonQuery<T>(ProcedureName procedureName, params ProcedureParameter[] parameters) where T : struct
        {
            var dbParams = await GetDbParameters(procedureName, parameters);
            var result = await dbMediator.ExecuteProcedureNonQuery(procedureName, dbParams);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public async Task ExecuteProcedureNoResult(ProcedureName procedureName, params ProcedureParameter[] parameters)
        {
            var dbParams = await GetDbParameters(procedureName, parameters);
            await dbMediator.ExecuteProcedureNonQuery(procedureName, dbParams);
        }

        private async Task<DataBaseParameter[]> GetDbParameters(ProcedureName procedureName, params ProcedureParameter[] parameters)
        {
            var result = new DataBaseParameter[parameters.Length];
            for(var i = 0; i < parameters.Length; ++i)
            {
                result[i] = await GetDbParameter(procedureName, parameters[i]);
            }
            return result;
        }

        private async Task<DataBaseParameter> GetDbParameter(ProcedureName procedureName, ProcedureParameter parameter)
        {
            var dbParam = new DataBaseParameter(parameter);
            if (parameter.DbType.HasValue)
                return dbParam;
            var procNameParam = new ProcedureParameter("procedureName", procedureName.GetStringName()) { DbType = SqlDbType.NVarChar };
            var res = (await ExecProcedure<ProcedureParameterDescription>(ProcedureName.UspInternalGetProcedureParameters, procNameParam)).Single();
            dbParam.DbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), res.ToString());
            return dbParam;
        }
    }
}
