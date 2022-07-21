using System;
using System.Data;
using System.Data.SqlClient;

namespace Server
{
    public static class SQLAccess
    {
        private static readonly string connStr = "Data Source=Localhost\\PARKINGLOTDB;Initial Catalog=ParkingLotDB;User ID=sa;pwd=Lusofona";

        public static DataTable ExecSqlDt(string storedProcName)
        {
            SqlConnection conn = new (connStr);
            conn.Open();
            SqlCommand sqlCommand = new (storedProcName, conn);
            DataTable dt = new ();
            dt.Load(sqlCommand.ExecuteReader());
            conn.Close();
            return dt;
        }

        public static string ExecSQLReturnData(string storedProcName)
        {
            var value = string.Empty;
            try
            {
                DataRow row = ExecSqlDt(storedProcName).Rows[0];
                value = row[0].ToString();
            }
            catch
            {

            }
            #pragma warning disable CS8603 // Possible null reference return.
            return value;
            #pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
