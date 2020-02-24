namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();
        /// <summary>
        /// Verfiying and placing of Order object into the database
        /// </summary>
        /// <param name="order">Partially assembled Order object</param>
        /// <param name="customerId">Cutomer ID of customer placing order</param>
        /// <returns>Bool verifying Save, if false, save did not occur and Amount cannot be 0</returns>
        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);
            OrderServiceModel result = OrderVerification(order, customer);
            if (result.Result)
            {
                orderRepository.Save(result.Order);
            }
            return result.Result;
        }
        /// <summary>
        /// Populates and tests the Order Placement
        /// </summary>
        /// <param name="order">The Order object that is being placed</param>
        /// <param name="customer">The customer Object that the order is being placed against</param>
        /// <returns>OrderServiceModel containing the populated Order object and whether the Order object meets the expectations</returns>
        public OrderServiceModel OrderVerification(Order order, Customer customer)
        {
            OrderServiceModel model = new OrderServiceModel()
            {
                Order = order
            };
            if (model.Order.Amount == 0)
                model.Result = false;

            if (customer.Country == "UK")
                model.Order.VAT = 0.2d;
            else
                model.Order.VAT = 0;

            model.Order.CustomerId = customer.CustomerId;
            return model;
        }
    }
}
