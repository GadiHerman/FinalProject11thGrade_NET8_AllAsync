using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingCart
    {
        public List<ProductsInCart> ProductList { get; set; }

        public ShoppingCart()
        {
            ProductList = new List<ProductsInCart>();
        }
    }
}
