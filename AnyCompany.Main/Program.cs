using System;
using System.Text.RegularExpressions;
using AnyCompany;

namespace AnyCompanyMain
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("Place Orders\n");
			Console.WriteLine("Please enter the User's ID: ");
			string userId = Console.ReadLine();
			while (!Regex.IsMatch(userId,@"^[0-9]+$"))
			{
				Console.WriteLine("User ID needs to be an integer");
				userId = Console.ReadLine();
			}
			Console.WriteLine("Please enter the amount on the order: ");
			string amount = Console.ReadLine();
			Order order = new Order()
			{
				Amount = Convert.ToDouble(amount),
				CustomerId = Convert.ToInt32(userId),
			};
			OrderService service = new OrderService();
			while (!service.PlaceOrder(order, Convert.ToInt32(userId)))
			{
				Console.WriteLine("Amount needs to be a number");
				userId = Console.ReadLine();
			}

			Console.WriteLine("Retrieve All Clients and their Orders\n");
			foreach(Customer item in CustomerRepository.AllClients())
			{
				Console.WriteLine("Custmer Name: " + item.Name);
				Console.WriteLine("Date of Birth: " + item.DateOfBirth);
				Console.WriteLine("Country: " + item.Country);
			}
						
		}
	}
}
