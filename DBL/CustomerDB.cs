using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Models;

namespace DBL
{
    public class CustomerDB : BaseDB<Customer>
    {
        protected override string GetTableName()
        {
            return "Customers";
        }
        protected override async Task<Customer> CreateModelAsync(object[] row)
        {
            Customer c = new Customer();
            c.Id = int.Parse(row[0].ToString());
            c.Name = row[1].ToString();
            c.Email = row[2].ToString();
            c.IsAdmin = bool.Parse(row[4].ToString());
            return c;
        }

        protected override async Task<List<Customer>> CreateListModelAsync(List<object[]> rows)
        {
            List<Customer> custList = new List<Customer>();
            foreach (object[] item in rows)
            {
                Customer c;
                c = (Customer)await CreateModelAsync(item);
                custList.Add(c);
            }
            return custList;
        }

        protected override async Task<Customer> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT customers.* FROM customers WHERE (CustomerID = @id)";
            AddParameterToCommand("@id", int.Parse(pk.ToString()));
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return ((List<Customer>)await SelectAllAsync());
        }

        public async Task<Customer> InsertGetObjAsync(Customer customer, string password)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "Name", customer.Name },
                { "Email", customer.Email },
                { "CustomerPassword", password }
            };
            return (Customer)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("Name", customer.Name);
            fillValues.Add("Email", customer.Email);
            filterValues.Add("CustomerID", customer.Id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Customer customer)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "CustomerID", customer.Id.ToString() }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<int> updatePasswordAsync(Customer customer, string password)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("Name", customer.Name);
            fillValues.Add("Email", customer.Email);
            fillValues.Add("CustomerPassword", password);
            filterValues.Add("CustomerID", customer.Id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<string> GetPasswordAsync(int id)
        {
            string sql = @"SELECT customers.CustomerPassword FROM customers WHERE (CustomerID = @id)";
            AddParameterToCommand("@id", id);
            string oldPassword = (string)await ExecScalarAsync(sql);
            return oldPassword;
        }

        public async Task<Customer> Login(string email, string password)
        {
            string sql = @"SELECT * FROM mystore.customers where Email=@email AND CustomerPassword=@password;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("email", email);
            p.Add("password", password);
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql,p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }


        public async Task<Customer> SelectByPkAsync(int id)
        {
            string sql = @"SELECT customers.* FROM customers WHERE (CustomerID = @id)";
            AddParameterToCommand("@id", id);
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<Customer> GetCustomerByOrderID(int orderID)
        {
            string sql = @$"Select mystore.customers.*
                           From mystore.customers Inner Join mystore.orders 
                           On mystore.orders.CustomerID = mystore.customers.CustomerID";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("mystore.orders.OrderID", orderID.ToString());
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql, p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<List<(string, string)>> GetNameAndEmail4NonAdmins()
        {
            List<(string, string)> returnList = new List<(string, string)>();
            string sql = "select * from Customers";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("IsAdmin", "0");
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql, p);
            foreach (Customer item in list)
            {
                returnList.Add((item.Name, item.Email));
            }
            return returnList;
        }

    }
}
