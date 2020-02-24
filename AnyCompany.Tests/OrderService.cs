using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        /// <summary>
        /// Test is the Amount is a Zero
        /// </summary>
        [TestMethod]
        public void TestOrderVerification1()
        {
            //Arrange
            Customer customer = new Customer()
            {
                Country = "UK",
                CustomerId = 1,
                DateOfBirth = DateTime.Now,
                Name = "Test 1"
            };
            OrderService orderService = new OrderService();
            Order order = new Order()
            {
                Amount = 0
            };
            //Act
            OrderServiceModel serviceModel = orderService.OrderVerification(order,customer);
            Assert.IsFalse(serviceModel.Result);
        }
        /// <summary>
        /// Test is the Amount is a not Zero
        /// </summary>
        [TestMethod]
        public void TestOrderVerification2()
        {
            //Arrange
            Customer customer = new Customer()
            {
                Country = "UK",
                CustomerId = 1,
                DateOfBirth = DateTime.Now,
                Name = "Test 1"
            };
            OrderService orderService = new OrderService();
            Order order = new Order()
            {
                Amount = 1
            };
            //Act
            OrderServiceModel serviceModel = orderService.OrderVerification(order, customer);
            Assert.IsTrue(serviceModel.Result);
        }
        /// <summary>
        /// Test that the VAT is assigned Correctly
        /// </summary>
        [TestMethod]
        public void TestOrderVerification3()
        {
            //Arrange
            Customer customer = new Customer()
            {
                Country = "UK",
                CustomerId = 1,
                DateOfBirth = DateTime.Now,
                Name = "Test 1"
            };
            OrderService orderService = new OrderService();
            Order order = new Order()
            {
                Amount = 1
            };
            //Act
            OrderServiceModel serviceModel = orderService.OrderVerification(order, customer);
            Assert.AreEqual(serviceModel.Order.VAT,02d);
        }
    }
}
