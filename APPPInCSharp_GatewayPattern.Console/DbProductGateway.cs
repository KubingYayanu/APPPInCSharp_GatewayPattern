using System;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_GatewayPattern
{
    public class DbProductGateway : ProductGateway
    {
        private readonly SqlConnection connection;

        public DbProductGateway(SqlConnection connection)
        {
            this.connection = connection;
        }

        public Product Find(string sku)
        {
            string sql = @"select * from Products where sku = @sku";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sku", sku);
            IDataReader reader = command.ExecuteReader();

            Product product = null;
            if (reader.Read())
            {
                string name = reader["name"].ToString();
                int price = Convert.ToInt32(reader["price"]);
                product = new Product(name, sku, price);
            }
            reader.Close();

            return product;
        }

        public void Insert(Product product)
        {
            string sql = @"insert into Products (sku, name, price) values (@sku, @name, @price)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sku", product.Sku);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.ExecuteNonQuery();
        }
    }
}