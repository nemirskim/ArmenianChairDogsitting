using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Tests;

public class OrdersControllerTests
{
    private OrdersController _sut;
    private Mock<IOrdersService> _ordersServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = APIMapperConfigStorage.GetInstance();
        _ordersServiceMock = new Mock<IOrdersService>();
        _sut = new OrdersController(_ordersServiceMock.Object, _mapper);
    }

    [Test]
    public void AddOrder_ValidRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var order = new OrderWalkRequest()
        {
            ClientId = 3,
            WalkQuantity = 2,
            Animals = new(),
            IsTrial = true,
            SitterId = 2,
            Status = Status.Created,
            Type = ServiceEnum.Walk
        };

        var expectedOrder = new OrderWalk
        {
            WalkQuantity = order.WalkQuantity,
            Animals = new(),
            Client = new() { Id = order.ClientId},
            Status = order.Status,
            Type = order.Type,
            IsTrial = order.IsTrial,
            Sitter = new() { Id = order.SitterId }
        };

        _ordersServiceMock
            .Setup(x => x.AddOrder(It.IsAny<Order>()))
            .Returns(expectedId);

        //when
        var actual = _sut.AddOrder(order);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);

        _ordersServiceMock.Verify(x => x.AddOrder(It.Is<OrderWalk>(c =>
            c.IsTrial == expectedOrder.IsTrial &&
            c.Type == expectedOrder.Type &&
            c.Status == expectedOrder.Status &&
            c.WalkQuantity == expectedOrder.WalkQuantity &&
            c.Sitter.Id == expectedOrder.Sitter.Id
        )), Times.Once);
    }

    [Test]
    public void ChangeOrderStatus_WhenValidParams_ThenNoContent()
    {
        //given
        var statusToParam = Status.InProgress;
        var idToParam = 3;

        _ordersServiceMock.Setup(x => x.UpdateOrderStatus(statusToParam, idToParam));

        //when
        var result = _sut.ChangeOrderStatus(statusToParam, idToParam);

        //
        var actualResult = result as NoContentResult;
        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
    }

    [Test]
    public void GetOrderById_WhenValidRequestPassed_ThenStatusCodeOk()
    {
        //given
        var expectedId = 3;

        var id = 3;

        var order = new OrderWalk
        {
            Id = 3,
            WalkQuantity = 1,
            Animals = new(),
            Client = new() { Id = 3 },
            Status = Status.Finished,
            Type = ServiceEnum.Walk,
            IsTrial = false,
            Sitter = new() { Id = 2 }
        };

        var expectedOrder = new OrderWalkResponse()
        {
            ClientId = order.Client.Id,
            WalkQuantity = order.WalkQuantity,
            Animals = new(),
            IsTrial = order.IsTrial,
            SitterId = order.Sitter.Id,
            Status = order.Status,
            Type = order.Type
        };


        _ordersServiceMock
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(order);

        //when
        var actual = _sut.GetOrderById(id);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult!.Value as OrderWalkResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedOrder.Status, actualValue.Status);
        Assert.AreEqual(expectedOrder.Type, actualValue.Type);
        Assert.AreEqual(expectedOrder.WalkQuantity, actualValue.WalkQuantity);
        Assert.AreEqual(expectedOrder.IsTrial, actualValue.IsTrial);
        Assert.AreEqual(expectedOrder.ClientId, actualValue.ClientId);
        Assert.AreEqual(expectedOrder.SitterId, actualValue.SitterId);
        Assert.AreEqual(expectedId, actualValue.Id);

        _ordersServiceMock.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetAllOrders_WhenValidRequestPassed_ThenStatusCodeOk()
    {
        //given
        var orders = new List<Order>()
        {
            new OrderWalk
            {
                Id = 3,
                WalkQuantity = 1,
                Animals = new(),
                Client = new() { Id = 3 },
                Status = Status.Finished,
                Type = ServiceEnum.Walk,
                IsTrial = false,
                Sitter = new() { Id = 2 }
            },

            new OrderWalk
            {
                Id = 2,
                WalkQuantity = 2,
                Animals = new(),
                Client = new() { Id = 1 },
                Status = Status.Created,
                Type = ServiceEnum.Walk,
                IsTrial = false,
                Sitter = new() { Id = 3 }
            },

            new OrderWalk
            {
                Id = 1,
                WalkQuantity = 1,
                Animals = new(),
                Client = new() { Id = 2 },
                Status = Status.InProgress,
                Type = ServiceEnum.Walk,
                IsTrial = false,
                Sitter = new() { Id = 1 }
            },
        };

        var expectedOrders = new List<AbstractOrderResponse>()
            {
            new OrderWalkResponse
            {
                Id = 3,
                WalkQuantity = 1,
                Animals = new(),
                ClientId = orders[0].Client.Id,
                Status = Status.Finished,
                Type = ServiceEnum.Walk,
                IsTrial = false,
                SitterId = orders[0].Sitter.Id
            },

            new OrderWalkResponse
            {
                Id = 2,
                WalkQuantity = 2,
                Animals = new(),
                ClientId = orders[1].Client.Id,
                Status = Status.Created,
                Type = ServiceEnum.Walk,
                IsTrial = false,
                SitterId = orders[1].Sitter.Id
            },

            new OrderWalkResponse
            {
                Id = 1,
                WalkQuantity = 1,
                Animals = new(),
                ClientId = orders[2].Client.Id,
                Status = Status.InProgress,
                Type = ServiceEnum.Walk,
                IsTrial = false,
                SitterId = orders[2].Sitter.Id
            },
        };

        _ordersServiceMock
            .Setup(x => x.GetAllOrders())
            .Returns(orders);

        //when
        var actual = _sut.GetAllOrders();

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value as List<AbstractOrderResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.IsTrue(actualValue is not null);
        Assert.AreEqual(expectedOrders.Count, actualValue.Count);
    }
}
