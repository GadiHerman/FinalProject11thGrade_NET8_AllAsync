using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(int orderItemID, int orderID, int productID, int quantity)
        {
            OrderItemID = orderItemID;
            OrderID = orderID;
            ProductID = productID;
            Quantity = quantity;
        }
    }
}
