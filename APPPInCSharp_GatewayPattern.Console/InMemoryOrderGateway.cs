using System.Collections;

namespace APPPInCSharp_GatewayPattern
{
    public class InMemoryOrderGateway : OrderGateway
    {
        private static int nextId = 1;
        private Hashtable orders = new Hashtable();

        public Order Find(int id) => orders[id] as Order;

        public void Insert(Order order)
        {
            orders[nextId++] = order;
        }
    }
}