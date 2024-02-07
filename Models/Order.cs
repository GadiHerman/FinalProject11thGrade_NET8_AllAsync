using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDateTime { get; set; }
        public List<ProductsInCart> ProductsInOrder { get; set; }

        public Customer CustomerDetails { get; set; }

        public Order() { }

        public Order(int id, DateTime orderDateTime,  List<ProductsInCart> productsInOrder, Customer customerDetails)
        {
            Id = id;
            OrderDateTime = orderDateTime;
            ProductsInOrder = productsInOrder;
            CustomerDetails = customerDetails;
        }
    }

}
