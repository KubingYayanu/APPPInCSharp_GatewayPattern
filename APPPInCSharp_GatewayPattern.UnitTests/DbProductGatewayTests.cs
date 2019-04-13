using NUnit.Framework;
using System.Data.SqlClient;

namespace APPPInCSharp_GatewayPattern.UnitTests
{
    [TestFixture]
    public class DbProductGatewayTests : AbstractDbGatewayTest
    {
        [SetUp]
        public void SetUp()
        {
            OpenConnection();
            gateway = new DbProductGateway(connection);
            ExecuteSql("delete from Products");
        }

        [TearDown]
        public void TearDown()
        {
            Close();
        }

        [Test]
        public void Insert()
        {
            Product product = new Product("Peanut Butter", "pb", 3);
            gateway.Insert(product);

            SqlCommand command = new SqlCommand("select * from Products", connection);
            reader = command.ExecuteReader();

            Assert.IsTrue(reader.Read());
            Assert.AreEqual("pb", reader["sku"]);
            Assert.AreEqual("Peanut Butter", reader["name"]);
            Assert.AreEqual(3, reader["price"]);
            Assert.IsFalse(reader.Read());
        }

        [Test]
        public void Find()
        {
            Product pb = new Product("Peanut Butter", "pb", 3);
            Product jam = new Product("Strawberry Jam", "jam", 2);
            gateway.Insert(pb);
            gateway.Insert(jam);

            Assert.IsNull(gateway.Find("bad sku"));

            Product foundPb = gateway.Find(pb.Sku);
            CheckThatProductsMatch(pb, foundPb);

            Product foundJam = gateway.Find(jam.Sku);
            CheckThatProductsMatch(jam, foundJam);
        }

        private static void CheckThatProductsMatch(Product pb, Product foundPb)
        {
            Assert.AreEqual(pb.Sku, foundPb.Sku);
            Assert.AreEqual(pb.Name, foundPb.Name);
            Assert.AreEqual(pb.Price, foundPb.Price);
        }
    }
}