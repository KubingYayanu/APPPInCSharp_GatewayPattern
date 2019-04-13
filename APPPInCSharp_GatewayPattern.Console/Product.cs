namespace APPPInCSharp_GatewayPattern
{
    public class Product
    {
        public Product(string name, string sku, int price)
        {
            this.name = name;
            this.sku = sku;
            this.price = price;
        }

        private readonly string name;
        private readonly string sku;
        private int price;

        public int Price => price;

        public string Name => name;

        public string Sku => sku;
    }
}