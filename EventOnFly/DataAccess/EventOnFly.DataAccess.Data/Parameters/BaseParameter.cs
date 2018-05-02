namespace EventOnFly.DataAccess.Data.Parameters
{
    public abstract class BaseParameter
    {
        protected BaseParameter(string parameterName, object obj)
        {
            ParameterName = $"{GetPrefix(parameterName)}{parameterName}";
            Object = obj;
        }

        public string ParameterName { get; }

        public object Object { get; }

        private static string GetPrefix(string inputName)
        {
            return inputName[0] == '@' ? "" : "@";
        }
    }
}
