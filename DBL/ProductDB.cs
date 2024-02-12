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
    public class ProductDB : BaseDB<Product>
    {
        protected override string GetTableName()
        {
            return "products";
        }

        protected override string GetPrimaryKeyName()
        {
            return "ProductID";
        }

        protected override async Task<Product> CreateModelAsync(object[] row)
        {
            Product p = new Product();
            p.ProductID = int.Parse(row[0].ToString());
            p.ProductName = row[1].ToString();
            p.ProductPrice = double.Parse(row[2].ToString());
            p.ProductTypeID = int.Parse(row[3].ToString());
            p.ProductURL = row[4].ToString();
            p.ProductIMG = (byte[])row[5];
            p.ProductIMGtype = row[6].ToString();
            return p;
        }

        protected override async Task<List<Product>> CreateListModelAsync(List<object[]> rows)
        {
            List<Product> custList = new List<Product>();
            foreach (object[] item in rows)
            {
                Product p;
                p = (Product)await CreateModelAsync(item);
                custList.Add(p);
            }
            return custList;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return ((List<Product>)await SelectAllAsync());
        }

        public async Task<bool> InsertAsync(Product product)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>
            {
                { "ProductName", product.ProductName },
                { "ProductPrice", product.ProductPrice.ToString() },
                { "ProductTypeID", product.ProductTypeID.ToString() },
                { "ProductURL", product.ProductURL.ToString() },
                { "ProductIMG", product.ProductIMG },
                { "ProductIMGtype", product.ProductIMGtype }
            };
            return await base.InsertAsync(fillValues) == 1;
        }

        public async Task<Product> InsertGetObjAsync(Product product)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "ProductName", product.ProductName },
                { "ProductPrice", product.ProductPrice.ToString() },
                { "ProductTypeID", product.ProductTypeID.ToString() },
                { "ProductURL", product.ProductURL.ToString() },
                { "ProductIMG", product.ProductIMG },
                { "ProductIMGtype", product.ProductIMGtype }
            };
            return (Product)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<int> UpdateAsync(Product product)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("ProductName", product.ProductName);
            fillValues.Add("ProductPrice", product.ProductPrice.ToString());
            fillValues.Add("ProductTypeID", product.ProductTypeID.ToString());
            fillValues.Add("ProductURL", product.ProductURL.ToString());
            filterValues.Add("ProductID", product.ProductID.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Product product)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("ProductID", product.ProductID.ToString());
            return await base.DeleteAsync(filterValues);
        }

        public async Task<int> updateImageAsync(int ProductID, byte[] imageArr, string ProductIMGtype)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("ProductIMG", imageArr.ToString());
            fillValues.Add("ProductIMGtype", ProductIMGtype);
            filterValues.Add("ProductID", ProductID.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<Product> SelectByPkAsync(int id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ProductID", id);
            List<Product> list = await SelectAllAsync(p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<List<Product>> GetBestProductAsync(int numOfProducts)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("numOfProducts", numOfProducts);
            string sql = @" Select products.*
                            From
                            products Inner Join
                            orderitems On orderitems.ProductID = products.ProductID
                            Group By
                                orderitems.ProductID
                            Order By
                                Count(orderitems.ProductID) Desc
                            LIMIT @numOfProducts;";
            return (List<Product>)await SelectAllAsync(sql,p);
        }

        public async Task<List<Product>> ProductSearch(string SearchString)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("s", SearchString);
            string sql = @"Select
                                products.*
                            From
                                products
                            Where
                                products.ProductName LIKE concat('%',@s,'%');";
            return (List<Product>)await SelectAllAsync(sql,p);
        }
    }
}
