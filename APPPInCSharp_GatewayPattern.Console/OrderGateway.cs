namespace APPPInCSharp_GatewayPattern
{
    public interface OrderGateway
    {
        void Insert(Order order);

        Order Find(int id);
    }
}