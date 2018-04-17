using System.Data;

namespace EventOnFly.Data.DbAccess.Parameters
{
    public class DataBaseParameter : BaseParameter
    {
        public DataBaseParameter(ProcedureParameter param) : base(param.ParameterName, param.Object)
        {
            if (param.DbType.HasValue)
                DbType = param.DbType.Value;
        }

        public SqlDbType DbType { get; set; }
    }
}
