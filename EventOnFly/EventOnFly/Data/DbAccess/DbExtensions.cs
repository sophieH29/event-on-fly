namespace EventOnFly.Data.DbAccess
{
    public static class DbExtensions
    {
        public static string GetStringName(this ProcedureName procedureName)
        {
            return $"[dbo].[{procedureName}]";
        }
    }
}
