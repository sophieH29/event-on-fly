using System.Data;

namespace EventOnFly.DataAccess.Data.Parameters
{
    public class ProcedureParameter: BaseParameter
    {
        public ProcedureParameter(string parameterName, object obj): base(parameterName, obj)
        {
        }

        public SqlDbType? DbType { get; set; }
    }
}
