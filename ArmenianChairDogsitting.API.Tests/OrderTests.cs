using NUnit.Framework;
using ArmenianChairDogsitting.API;
using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Controllers;

namespace ArmenianChairDogsitting.API.Tests
{
    public class OrderTests
    {
        [SetUp]
        public void Setup()
        {
            var orderController = new OrdersController();
        }

        [TestCase(OrdersEnums.Created1, ResultEnum.Created)]
        [TestCase(OrdersEnums.BadCreated, ResultEnum.UnprocessableEntity)]
        public void AddOrderTest(OrdersEnums orderType, ResultEnum expectedResultType)
        {
            var orderController = new OrdersController();
            var order = OrdersMock.GetRequestMock(orderType);
            var expectedResult = ResultMocks.GetMock(expectedResultType);
            var actual = orderController.AddOrder(order);
            Assert.AreEqual(expectedResult, actual);
        }
    }
}