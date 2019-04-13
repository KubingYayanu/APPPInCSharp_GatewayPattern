namespace APPPInCSharp_GatewayPattern
{
    public interface ProductGateway
    {
        void Insert(Product product);

        Product Find(string sku);
    }
}