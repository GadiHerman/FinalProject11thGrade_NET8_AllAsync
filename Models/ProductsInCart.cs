using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductsInCart: Product
    {
        public int Quantity { get; set; }        
        public ProductsInCart(int productID, string productName, double productPrice ,int Quantity) : base(productID, productName, productPrice)
        {
            this.Quantity = Quantity;
        }

        public ProductsInCart() : base()
        {
        }

    }
}
