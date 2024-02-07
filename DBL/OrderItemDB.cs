using Models;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class OrderItemDB : BaseDB<OrderItem>
    {
        protected override string GetTableName()
        {
            return "orderitems";
        }

        protected override async Task<OrderItem> CreateModelAsync(object[] row)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemID = int.Parse(row[0].ToString());
            orderItem.OrderID = int.Parse(row[1].ToString());
            orderItem.ProductID = int.Parse(row[2].ToString());
            orderItem.Quantity = int.Parse(row[3].ToString());
            return orderItem;
        }

        protected override async Task<List<OrderItem>> CreateListModelAsync(List<object[]> rows)
        {
            List<OrderItem> orderList = new List<OrderItem>();
            foreach (object[] item in rows)
            {
                OrderItem orderItem = new OrderItem();
                orderItem = (OrderItem)await CreateModelAsync(item);
                orderList.Add(orderItem);
            }
            return orderList;
        }
        protected override async Task<OrderItem> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT orderitems.* FROM orderitems WHERE (OrderItemID = @id)";
            AddParameterToCommand("@id", int.Parse(pk.ToString()));
            List<OrderItem> list = (List<OrderItem>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<bool> InsertAsync(OrderItem orderItem)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            fillValues.Add("OrderID", orderItem.OrderID.ToString());
            fillValues.Add("ProductID", orderItem.ProductID.ToString());
            fillValues.Add("Quantity", orderItem.Quantity.ToString());
            int a = await base.InsertAsync(fillValues);
            return  (a== 1);
        }

        //public async Task<int> UpdateAsync(OrderItem order)
        //{
        //    Dictionary<string, object> fillValues = new Dictionary<string, object>();
        //    Dictionary<string, object> filterValues = new Dictionary<string, object>();
        //    DateTime d = DateTime.Now;
        //    string dts = $"'{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}'";
        //    fillValues.Add("OrderDateTime", dts);
        //    filterValues.Add("OrderID", order.Id.ToString());
        //    return await base.UpdateAsync(fillValues, filterValues);
        //}

        //public async Task<int> DeleteAsync(OrderItem order)
        //{
        //    Dictionary<string, object> filterValues = new Dictionary<string, object>();
        //    filterValues.Add("OrderID", order.Id.ToString());
        //    return await base.DeleteAsync(filterValues);
        //}


        //public async Task<List<OrderItem>> GetAllOrdersByCustomerIDAsync(int customerID)
        //{
        //    Dictionary<string, object> p = new Dictionary<string, object>();
        //    p.Add("CustomerID", customerID.ToString());
        //    return (List<OrderItem>)await SelectAllAsync(p);
        //}

        //public async Task<OrderItem> GetOrderByPkAsync(int OrderID)
        //{
        //    Dictionary<string, object> p = new Dictionary<string, object>();
        //    p.Add("OrderID", OrderID.ToString());
        //    List<OrderItem> list = (List<OrderItem>)await SelectAllAsync(p);
        //    if (list.Count == 1)
        //        return list[0];
        //    else
        //        return null;
        //}

        //public async Task<List<OrderItem>> GetAllAsync()
        //{
        //    return (List<OrderItem>)await SelectAllAsync();
        //}

        //public async Task<OrderItem> InsertGetObjAsync(OrderItem order)
        //{
        //    Dictionary<string, object> fillValues = new Dictionary<string, object>();
        //    DateTime d = DateTime.Now;
        //    string dts = $"{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}";
        //    fillValues.Add("OrderDateTime", dts);
        //    fillValues.Add("CustomerID", order.CustomerDetails.Id.ToString());
        //    OrderItem returnOrder = (OrderItem)await base.InsertGetObjAsync(fillValues);

        //    return returnOrder;
        //}


    }
}
