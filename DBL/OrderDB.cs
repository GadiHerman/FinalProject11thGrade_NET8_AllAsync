using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class OrderDB : BaseDB<Order>
    {
        protected override string GetTableName()
        {
            return "Orders";
        }

        protected override async Task<Order> CreateModelAsync(object[] row)
        {
            Order order = new Order();
            order.Id = int.Parse(row[0].ToString());
            order.OrderDateTime = DateTime.Parse(row[1].ToString());
            int custID = int.Parse(row[2].ToString());
            Customer customer = new Customer();
            CustomerDB customerDB = new CustomerDB();
            customer = (Customer)await customerDB.SelectByPkAsync(custID);
            order.CustomerDetails = customer;
            ProductsInCartDB productsDB = new ProductsInCartDB();
            order.ProductsInOrder = await productsDB.GetProductsInOrderAsync(order.Id);
            return order;
        }

        protected override async Task<List<Order>> CreateListModelAsync(List<object[]> rows)
        {
            List<Order> orderList = new List<Order>();
            foreach (object[] item in rows)
            {
                Order o = new Order();
                o = (Order)await CreateModelAsync(item);
                orderList.Add(o);
            }
            return orderList;
        }
        protected override async Task<Order> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT orders.* FROM orders WHERE (OrderID = @id)";
            AddParameterToCommand("@id", int.Parse(pk.ToString()));
            List<Order> list = (List<Order>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return (List<Order>)await SelectAllAsync();
        }

        public async Task<List<Order>> GetAllOrdersByCustomerIDAsync(int customerID)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("CustomerID", customerID.ToString());
            return (List<Order>)await SelectAllAsync(p);
        }
        public async Task<Order> GetOrderByPkAsync(int OrderID)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("OrderID", OrderID.ToString());
            List<Order> list = (List<Order>)await SelectAllAsync(p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        //public async Task<bool> InsertAsync(Order order)
        //{
        //    Dictionary<string, object> fillValues = new Dictionary<string, object>();
        //    DateTime d = DateTime.Now;
        //    string dts = $"'{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}'";
        //    fillValues.Add("OrderDateTime", dts);
        //    fillValues.Add("CustomerID", order.customer.Id.ToString());
        //    return await base.InsertAsync(fillValues) == 1;
        //}

        public async Task<Order> InsertGetObjAsync(Order order)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            DateTime d = DateTime.Now;
            string dts = $"{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}";
            fillValues.Add("OrderDateTime", dts);
            fillValues.Add("CustomerID", order.CustomerDetails.Id.ToString());
            Order returnOrder = (Order)await base.InsertGetObjAsync(fillValues);

            return returnOrder;
        }

        public async Task<int> UpdateAsync(Order order)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            DateTime d = DateTime.Now;
            string dts = $"{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}";
            fillValues.Add("OrderDateTime", dts);
            filterValues.Add("OrderID", order.Id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Order order)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("OrderID", order.Id.ToString());
            return await base.DeleteAsync(filterValues);
        }
    }
}
