using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustmerId", order.CustomerId);
            command.ExecuteNonQuery();

            connection.Close();
        }
        public List<Order> GetOrders(int customerId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE CustomerId =" + customerId,
                connection);
            var reader = command.ExecuteReader();
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                Order order = new Order()
                {
                    Amount = Convert.ToDouble(reader["Amount"]),
                    OrderId = Convert.ToInt32(reader["OrderId"]),
                    VAT= Convert.ToDouble(reader["VAT"]),
                    CustomerId = customerId
                };
                orders.Add(order);
            }
            return orders;
        }
    }
}
