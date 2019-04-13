using System;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_GatewayPattern
{
    public class DbOrderGateway : OrderGateway
    {
        private readonly ProductGateway productGateway;
        private readonly SqlConnection connection;

        public DbOrderGateway(SqlConnection connection, ProductGateway productGateway)
        {
            this.connection = connection;
            this.productGateway = productGateway;
        }

        public Order Find(int id)
        {
            string sql = @"select * from Orders where orderId = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            IDataReader reader = command.ExecuteReader();

            Order order = null;
            if (reader.Read())
            {
                string customerId = reader["cusId"].ToString();
                order = new Order(customerId);
                order.Id = id;
            }
            reader.Close();

            if (order != null)
            {
                LoadItems(order);
            }

            return order;
        }

        private void LoadItems(Order order)
        {
            string sql = @"select * from Items where orderId = @orderId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderId", order.Id);
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string sku = reader["sku"].ToString();
                int quantity = Convert.ToInt32(reader["quantity"]);
                Product product = productGateway.Find(sku);
                order.AddItem(product, quantity);
            }
        }

        public void Insert(Order order)
        {
            string sql = @"insert into Orders (cusId) values (@cusId);
                            select scope_identity()";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@cusId", order.CustomerId);
            int id = Convert.ToInt32(command.ExecuteScalar());
            order.Id = id;

            InsertItems(order);
        }

        private void InsertItems(Order order)
        {
            string sql = @"insert into Items (orderId, quantity, sku) values (@orderId, @quantity, @sku)";
            foreach (var item in order.Items)
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@orderId", order.Id);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                command.Parameters.AddWithValue("@sku", item.Product.Sku);
                command.ExecuteNonQuery();
            }
        }
    }
}