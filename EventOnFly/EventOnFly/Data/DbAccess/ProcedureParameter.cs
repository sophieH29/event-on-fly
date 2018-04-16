namespace EventOnFly.Data.DbAccess
{
    public class ProcedureParameter
    {
        public ProcedureParameter(string parameterName, object obj)
        {
            ParameterName = $"{GetPrefix(parameterName)}{parameterName}";
            Object = obj;
        }

        public string ParameterName { get; }

        public object Object { get; }

        private string GetPrefix(string inputName)
        {
            return inputName[0] == '@' ? "" : "@";
        }
    }
}
