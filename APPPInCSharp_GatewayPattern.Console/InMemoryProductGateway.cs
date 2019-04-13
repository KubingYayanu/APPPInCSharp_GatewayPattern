using System.Collections;

namespace APPPInCSharp_GatewayPattern
{
    public class InMemoryProductGateway : ProductGateway
    {
        private Hashtable products = new Hashtable();

        public Product Find(string sku) => products[sku] as Product;

        public void Insert(Product product)
        {
            products[product.Sku] = product;
        }
    }
}