using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Models;
using Org.BouncyCastle.Utilities;

namespace DBL
{
    public class ProductsInCartDB : ProductDB
    {
        protected override async Task<Product> CreateModelAsync(object[] row)
        {
            Product p = new ProductsInCart();
            p.ProductID = int.Parse(row[0].ToString());
            p.ProductName = row[1].ToString();
            p.ProductPrice = double.Parse(row[2].ToString());
            p.ProductTypeID = int.Parse(row[3].ToString());
            p.ProductURL = row[4].ToString();
            p.ProductIMG = (byte[])row[5];
            p.ProductIMGtype = row[6].ToString();
            ((ProductsInCart)p).Quantity = int.Parse(row[7].ToString());
            return p;
        }

        public async Task<List<ProductsInCart>> GetProductsInOrderAsync(int orderID)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", orderID);
            string sql = @"Select
                                products.*,
                                orderitems.Quantity
                            From
                                products Inner Join
                                orderitems On orderitems.ProductID = products.ProductID
                            Where
                                orderitems.OrderID = @id;";
            List<Product> products = await SelectAllAsync(sql, p);
            List<ProductsInCart> pp = new List<ProductsInCart>();
            foreach (Product product in products)
            {
                pp.Add((ProductsInCart)product);
            }
            return pp;
        }
    }
}
