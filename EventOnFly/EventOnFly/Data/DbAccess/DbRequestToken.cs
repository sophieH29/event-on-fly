using System;
using System.Data.SqlClient;

namespace EventOnFly.Data.DbAccess
{
    public class DbRequestToken: IDisposable
    {
        public DbRequestToken(SqlConnection connection, SqlTransaction transaction)
        {
            Connection = connection;
            Transaction = transaction;
        }

        public SqlConnection Connection { get; }

        public SqlTransaction Transaction { get; }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Dispose()
        {
            Transaction.Dispose();
            Connection.Close();
        }
    }
}
