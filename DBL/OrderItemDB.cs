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

        protected override string GetPrimaryKeyName()
        {
            return "OrderItemID";
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

        public async Task<bool> InsertAsync(OrderItem orderItem)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            fillValues.Add("OrderID", orderItem.OrderID.ToString());
            fillValues.Add("ProductID", orderItem.ProductID.ToString());
            fillValues.Add("Quantity", orderItem.Quantity.ToString());
            int a = await base.InsertAsync(fillValues);
            return  (a== 1);
        }
    }
}
