using DBL;
using Models;
using System.Collections.Generic;

namespace ConsoleUnitTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Test Insert new customer Async
            CustomerDB db = new CustomerDB();
            Customer customer = new Customer();
            customer.Name = "tal";
            customer.Email = "tal@gmail.com";
            customer = await db.InsertGetObjAsync(customer, "1234");
            await Console.Out.WriteLineAsync($"id: {customer.Id} name: {customer.Name} email: {customer.Email}\n\n");
 
            //TEST Get All Customers
            List<Customer> list = await db.GetAllAsync();
            for (int i = 0; i < list.Count; i++)
            {
                await Console.Out.WriteLineAsync(@$" id={list[i].Id} name={list[i].Name} email={list[i].Email}");
            }
            await Console.Out.WriteLineAsync("\n\n");
            
            //TEST Get Customer by OrderID
            customer = await db.GetCustomerByOrderID(1);
            await Console.Out.WriteLineAsync($" OrderID=1 id: {customer.Id} name: {customer.Name} email: {customer.Email}\n\n");

            //TEST CustomerDB Get by id
            customer = await db.SelectByPkAsync(1);
            if (list != null)
            {
                await Console.Out.WriteLineAsync($" name: {list[0].Name} email: {list[0].Email}\n\n");
            }

            //TEST Get data from DB When there is no data structure ready 
            List<(string, string)> lst = await db.GetNameAndEmail4NonAdmins();  
            foreach (var item in lst)
            {
                await Console.Out.WriteAsync($" {item.Item1} - {item.Item2}  \n");
            }
            await Console.Out.WriteLineAsync("\n\n");

            ProductsInCartDB pdb = new ProductsInCartDB();
            List<ProductsInCart> lst1  = await pdb.GetProductsInOrderAsync(1);
            foreach (var item in lst1)
            {
                await Console.Out.WriteAsync($" {item.ProductName} - {item.Quantity}   \n");
            }
            await Console.Out.WriteLineAsync("\n\n");
        }
    }
}
