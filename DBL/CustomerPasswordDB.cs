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
    public class CustomerPasswordDB : CustomerDB
    {

        protected override async Task<Customer> CreateModelAsync(object[] row)
        {
            Customer c = new CustomerPassword();
            c.Id = int.Parse(row[0].ToString());
            c.Name = row[1].ToString();
            c.Email = row[2].ToString();
            ((CustomerPassword)c).Password = row[3].ToString();
            c.IsAdmin = bool.Parse(row[4].ToString()); 
            return c;
        }

        public async Task<CustomerPassword> SelectByPkAsync(int id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("CustomerID", id);
            List<Customer> list = await SelectAllAsync(p);
            if (list.Count == 1)
                return (CustomerPassword)list[0];
            else
                return null;
        }

        public async Task<int> updatePasswordAsync(CustomerPassword customer)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("Name", customer.Name);
            fillValues.Add("Email", customer.Email);
            fillValues.Add("CustomerPassword", customer.Password);
            filterValues.Add("CustomerID", customer.Id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<Customer> LoginAsync(string email, string password)
        {
            string sql = @"SELECT * FROM mystore.customers where Email=@email AND CustomerPassword=@password;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("email", email);
            p.Add("password", password);
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql, p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

    }
}
