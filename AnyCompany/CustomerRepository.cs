using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";
        /// <summary>
        /// Client object retrieval
        /// </summary>
        /// <param name="customerId">Customer ID being queried</param>
        /// <returns>Client object as per specified ID</returns>
        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }
        /// <summary>
        /// Client List with Orders
        /// </summary>
        /// <returns>A List of all client objects and their accosiated orders</returns>
        public static List<Customer> AllClients()
        {
            OrderRepository orderRepository = new OrderRepository();
            
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer",connection);
            var reader = command.ExecuteReader();
            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                Customer customer = new Customer()
                {
                    Name = reader["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    Country = reader["Country"].ToString(),
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    Orders = orderRepository.GetOrders(Convert.ToInt32(reader["CustomerId"]))
                };
                customers.Add(customer);
            }
            return customers;
        }
    }
}
