namespace APPPInCSharp_GatewayPattern
{
    public class Item
    {
        public Item(Product p, int qty)
        {
            product = p;
            quantity = qty;
        }

        private Product product;
        private int quantity;

        public Product Product => product;

        public int Quantity => quantity;
    }
}