using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventOnFly.Data.DbAccess
{
    public interface IProcedureExecutor
    {
        Task<IEnumerable<T>> ExecProcedure<T>(ProcedureName procedureName, params object[] parameters) where T : new();

        Task<T> ExecProcedureNonQuery<T>(ProcedureName procedureName, params object[] parameters) where T : struct;

        Task ExecuteProcedureNoResult(ProcedureName procedureName, params object[] parameters);
    }

    public class ProcedureExecutor : IProcedureExecutor
    {
        private readonly IDbMediator dbMediator;

        public ProcedureExecutor(IDbMediator dbMediator)
        {
            this.dbMediator = dbMediator;
        }

        public async Task<IEnumerable<T>> ExecProcedure<T>(ProcedureName procedureName, params object[] parameters) where T : new()
        {
            var result = await dbMediator.ExecuteProcedure(procedureName, parameters);
            return result.MapToType<T>();
        }

        public async Task<T> ExecProcedureNonQuery<T>(ProcedureName procedureName, params object[] parameters) where T : struct
        {
            var result = await dbMediator.ExecuteProcedureNonQuery(procedureName, parameters);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public async Task ExecuteProcedureNoResult(ProcedureName procedureName, params object[] parameters)
        {
            await dbMediator.ExecuteProcedureNonQuery(procedureName, parameters);
        }
    }
}
