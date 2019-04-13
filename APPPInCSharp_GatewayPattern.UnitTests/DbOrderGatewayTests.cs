using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace APPPInCSharp_GatewayPattern.UnitTests
{
    [TestFixture]
    public class DbOrderGatewayTests : AbstractDbGatewayTest
    {
        private DbOrderGateway orderGateway;
        private Product pizza;
        private Product beer;

        [SetUp]
        public void SetUp()
        {
            OpenConnection();
            pizza = new Product("Pizza", "pizza", 15);
            beer = new Product("Beer", "beer", 2);
            ProductGateway productGateway = new InMemoryProductGateway();
            productGateway.Insert(pizza);
            productGateway.Insert(beer);

            orderGateway = new DbOrderGateway(connection, productGateway);
            ExecuteSql("delete from Orders");
            ExecuteSql("delete from Items");
        }

        [TearDown]
        public void TearDown()
        {
            Close();
        }

        [Test]
        public void Find()
        {
            string sql = @"insert into Orders (cusId) values ('Snoopy');
                            select scope_identity()";
            SqlCommand command = new SqlCommand(sql, connection);
            int orderId = Convert.ToInt32(command.ExecuteScalar());
            ExecuteSql($@"insert into Items (orderId, quantity, sku) values ({orderId}, 1, 'pizza')");
            ExecuteSql($@"insert into Items (orderId, quantity, sku) values ({orderId}, 6, 'beer')");
            Order order = orderGateway.Find(orderId);

            Assert.AreEqual("Snoopy", order.CustomerId);
            Assert.AreEqual(2, order.ItemCount);
            Assert.AreEqual(1, order.QuantityOf(pizza));
            Assert.AreEqual(6, order.QuantityOf(beer));
        }

        [Test]
        public void Insert()
        {
            Order order = new Order("Snoopy");
            order.AddItem(pizza, 1);
            order.AddItem(beer, 6);

            orderGateway.Insert(order);

            Assert.IsTrue(order.Id != -1);

            Order foundOrder = orderGateway.Find(order.Id);

            Assert.AreEqual("Snoopy", foundOrder.CustomerId);
            Assert.AreEqual(2, foundOrder.ItemCount);
            Assert.AreEqual(1, foundOrder.QuantityOf(pizza));
            Assert.AreEqual(6, foundOrder.QuantityOf(beer));
        }
    }
}