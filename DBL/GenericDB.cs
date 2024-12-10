using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class GenericDB : BaseDB<object>
    {
        protected async override Task<object> CreateModelAsync(object[] row)
        {
            return row;
        }

        protected override string GetPrimaryKeyName()
        {
            throw new NotImplementedException();
        }

        protected override string GetTableName()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductPresentation>> GetBestProductList()
        {
            List<ProductPresentation> ProductPresentationList = new List<ProductPresentation>();
            List<object> list;
            object[] objects;
            string sql = @" Select
                                products.ProductName,
                                producttypes.ProductTypeName As ProductType,
                                Sum(orderitems.Quantity) As QuantitySold
                            From
                                producttypes Inner Join
                                products On products.ProductTypeID = producttypes.ProductTypeID Inner Join
                                orderitems On orderitems.ProductID = products.ProductID
                            Group By
                                products.ProductName,
                                producttypes.ProductTypeName
                            Order By
                                QuantitySold Desc
                            limit 3";
            sql = sql.Replace("\r\n", string.Empty);
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("item", itemName);
            //list = await SelectAllAsync(sql, dic);
            list = await SelectAllAsync(sql);
            if (list != null)
            {
                foreach (object obj in list)
                {
                    objects = (object[])obj;

                    ProductPresentation product = new ProductPresentation();
                    product.ProductName = objects[0].ToString();
                    product.ProductType = objects[1].ToString();
                    product.QuantitySold = int.Parse(objects[2].ToString());
                    ProductPresentationList.Add(product);
                }
            }
            else { return null; }
            return ProductPresentationList;
        }
    }
}
