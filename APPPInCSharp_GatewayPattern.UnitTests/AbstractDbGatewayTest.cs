using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_GatewayPattern.UnitTests
{
    public class AbstractDbGatewayTest
    {
        protected SqlConnection connection;
        protected DbProductGateway gateway;
        protected IDataReader reader;

        protected void ExecuteSql(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        protected void OpenConnection()
        {
            string connectionString = @"Initial Catalog=QuickMart;Data Source=10.0.75.1;user id=dev;password=sa;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        protected void Close()
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}