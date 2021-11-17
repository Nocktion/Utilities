using Nockdev.Database.Data;
using System.Data.SqlClient;

namespace Nockdev.Database
{
    public class MSSQL
    {
        public SqlConnection conn;

        public SqlConnection CreateConnection(string server, string username, string password)
        {
            conn = new SqlConnection(string.Concat("Data Source=", server, ";User ID=", username, ";Password=", password, ";ApplicationIntent=ReadWrite;"));
            conn.Open();
            return conn;
        }

        public SqlConnection CreateConnection(string server, string database, string username, string password)
        {
            conn = new SqlConnection(string.Concat("Data Source=", server, ";Database=", database, ";User ID=", username, ";Password=", password, ";ApplicationIntent=ReadWrite;"));
            conn.Open();
            return conn;
        }

        ~MSSQL()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public SqlDataReader QueryRead(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            return cmd.ExecuteReader();
        }

        public int Query(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            return cmd.ExecuteNonQuery();
        }

        public void Close()
        {
            conn.Close();
        }
    }
}
