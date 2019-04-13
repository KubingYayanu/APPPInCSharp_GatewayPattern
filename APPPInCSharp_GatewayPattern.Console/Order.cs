using System.Collections.Generic;

namespace APPPInCSharp_GatewayPattern
{
    public class Order
    {
        public Order(string cusId)
        {
            this.cusId = cusId;
        }

        private readonly string cusId;
        private List<Item> items = new List<Item>();
        private int id;

        public string CustomerId => cusId;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ItemCount => items.Count;

        public int QuantityOf(Product product)
        {
            foreach (var item in items)
            {
                if (item.Product.Sku.Equals(product.Sku))
                {
                    return item.Quantity;
                }
            }
            return 0;
        }

        public void AddItem(Product p, int qty)
        {
            var item = new Item(p, qty);
            items.Add(item);
        }

        public List<Item> Items => items;

        public int Total
        {
            get
            {
                int total = 0;
                foreach (var item in items)
                {
                    Product p = item.Product;
                    int qty = item.Quantity;
                    total += p.Price * qty;
                }
                return total;
            }
        }
    }
}