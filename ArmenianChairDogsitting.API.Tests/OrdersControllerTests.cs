﻿using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ArmenianChairDogsitting.API.Tests;

public class OrdersControllerTests
{
    private OrdersController _sut;
    private Mock<IOrdersService> _ordersServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new APIMapperConfigStorage());
        });
        _mapper = mapper.CreateMapper();
        _ordersServiceMock = new Mock<IOrdersService>();
        _sut = new OrdersController(_ordersServiceMock.Object, _mapper);
    }

    [Test]
    public void AddOrderWalk_ValidRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var order = new OrderWalkRequest()
        {
            ClientId = 3,
            SitterId = 2,
            Status = Status.Created,
            AnimalIds = new(),
            District = Data.Enums.District.All,
            WorkDate = new DateTime(2022, 8, 25),
            IsTrial = true,
        };

        var expectedOrder = new Order
        {
            Client = new() { Id = order.ClientId},
            Sitter = new() { Id = order.SitterId },
            Status = order.Status,
            Animals = new(),
            District = order.District,
            WorkDate = order.WorkDate,
            IsTrial = order.IsTrial
        };

        _ordersServiceMock
            .Setup(x => x.AddOrder(It.IsAny<Order>(), Service.Walk))
            .Returns(expectedId);

        //when
        var actual = _sut.AddOrder(order);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);

        _ordersServiceMock.Verify(x => x.AddOrder(
            It.Is<Order>(
                c => c.Type == expectedOrder.Type &&
                c.Status == expectedOrder.Status &&
                c.Client.Id == expectedOrder.Client.Id &&
                c.Sitter.Id == expectedOrder.Sitter.Id &&
                c.DayQuantity == null &&
                c.WalkPerDayQuantity == null &&
                c.District == expectedOrder.District &&
                c.WorkDate == expectedOrder.WorkDate &&
                c.VisitQuantity == null &&
                c.WalkQuantity == expectedOrder.WalkQuantity &&
                c.IsTrial == expectedOrder.IsTrial),
            Service.Walk
            ), Times.Once);
    }

    [Test]
    public void AddOrderOverexpose_ValidRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var order = new OrderOverexposeRequest()
        {
            ClientId = 3,
            SitterId = 2,
            DayQuantity = 2,
            WalkPerDayQuantity = 1,
            AnimalIds = new(),
            District = Data.Enums.District.All,
            Status = Status.Created,
            WorkDate = new DateTime(2022, 8, 25)
        };

        var expectedOrder = new Order
        {
            Client = new() { Id = order.ClientId },
            Sitter = new() { Id = order.SitterId },
            Status = order.Status,
            DayQuantity = order.DayQuantity,
            WalkPerDayQuantity = order.WalkPerDayQuantity,
            Animals = new(),
            District = order.District,
            WorkDate = order.WorkDate
        };

        _ordersServiceMock
            .Setup(x => x.AddOrder(It.IsAny<Order>(), Service.Overexpose))
            .Returns(expectedId);

        //when
        var actual = _sut.AddOrder(order);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);

        _ordersServiceMock.Verify(x => x.AddOrder(
            It.Is<Order>(
                c => c.Type == expectedOrder.Type &&
                c.Status == expectedOrder.Status &&
                c.Client.Id == expectedOrder.Client.Id &&
                c.Sitter.Id == expectedOrder.Sitter.Id &&
                c.DayQuantity == expectedOrder.DayQuantity &&
                c.WalkPerDayQuantity == expectedOrder.WalkPerDayQuantity &&
                c.District == expectedOrder.District &&
                c.WorkDate == expectedOrder.WorkDate &&
                c.VisitQuantity == null &&
                c.WalkQuantity == null &&
                c.IsTrial == null),
            Service.Overexpose
            ), Times.Once);
    }

    [Test]
    public void AddOrderDailySitting_ValidRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var order = new OrderDailySittingRequest()
        {
            ClientId = 3,
            SitterId = 2,
            Status = Status.Created,
            WalkPerDayQuantity = 2,
            DayQuantity = 1,
            AnimalIds = new(),
            District = Data.Enums.District.All,
            WorkDate = new DateTime(2022, 8, 25)
        };

        var expectedOrder = new Order
        {
            Client = new() { Id = order.ClientId },
            Sitter = new() { Id = order.SitterId },
            Status = order.Status,
            WalkQuantity = order.WalkPerDayQuantity,
            DayQuantity = order.DayQuantity,
            Animals = new(),
            District = order.District,
            WorkDate = order.WorkDate
        };

        _ordersServiceMock
            .Setup(x => x.AddOrder(It.IsAny<Order>(), Service.DailySitting))
            .Returns(expectedId);

        //when
        var actual = _sut.AddOrder(order);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);

        _ordersServiceMock.Verify(x => x.AddOrder(
            It.Is<Order>(
                c => c.Type == expectedOrder.Type &&
                c.Status == expectedOrder.Status &&
                c.Client.Id == expectedOrder.Client.Id &&
                c.Sitter.Id == expectedOrder.Sitter.Id &&
                c.DayQuantity == expectedOrder.DayQuantity &&
                c.WalkPerDayQuantity == null &&
                c.District == expectedOrder.District &&
                c.WorkDate == expectedOrder.WorkDate &&
                c.VisitQuantity == null &&
                c.WalkQuantity == expectedOrder.WalkQuantity &&
                c.IsTrial == null),
            Service.DailySitting
            ), Times.Once);
    }

    [Test]
    public void AddOrderSittingForDay_ValidRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var order = new OrderSittingForDayRequest()
        {
            ClientId = 3,
            SitterId = 2,
            Status = Status.Created,
            VisitQuantity = 3,
            AnimalIds = new(),
            District = Data.Enums.District.All,
            WorkDate = new DateTime(2022, 8, 25)
        };

        var expectedOrder = new Order
        {
            Client = new() { Id = order.ClientId },
            Sitter = new() { Id = order.SitterId },
            Status = order.Status,
            VisitQuantity = order.VisitQuantity,
            Animals = new(),
            District = order.District,
            WorkDate = order.WorkDate
        };

        _ordersServiceMock
            .Setup(x => x.AddOrder(It.IsAny<Order>(), Service.SittingForDay))
            .Returns(expectedId);

        //when
        var actual = _sut.AddOrder(order);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);

        _ordersServiceMock.Verify(x => x.AddOrder(
            It.Is<Order>(
                c => c.Type == expectedOrder.Type &&
                c.Status == expectedOrder.Status &&
                c.Client.Id == expectedOrder.Client.Id &&
                c.Sitter.Id == expectedOrder.Sitter.Id &&
                c.DayQuantity == null &&
                c.WalkPerDayQuantity == null &&
                c.District == expectedOrder.District &&
                c.WorkDate == expectedOrder.WorkDate &&
                c.VisitQuantity == expectedOrder.VisitQuantity &&
                c.WalkQuantity == expectedOrder.WalkQuantity &&
                c.IsTrial == null),
            Service.SittingForDay
            ), Times.Once);
    }

    [Test]
    public void ChangeOrderStatus_WhenValidParams_ThenNoContent()
    {
        //given
        var statusToParam = Status.InProgress;
        var idToParam = 3;

        //when
        var result = _sut.ChangeOrderStatus(statusToParam, idToParam);

        //then
        var actualResult = result as NoContentResult;
        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _ordersServiceMock.Verify(x => x.UpdateOrderStatus(statusToParam, idToParam), Times.Once);
    }

    [Test]
    public void GetOrderById_WhenValidRequestPassed_ThenStatusCodeOk()
    {
        //given
        var expectedId = 3;

        var id = 3;

        var order = new Order
        {
            Id = 3,
            WalkPerDayQuantity = 1,
            Animals = new(),
            Client = new() { Id = 3 },
            Status = Status.Finished,
            Type = Service.Walk,
            IsTrial = false,
            Sitter = new() { Id = 2 }
        };

        var expectedOrder = new OrderResponse()
        {
            ClientId = order.Client.Id,
            WalkPerDayQuantity = (int)order.WalkPerDayQuantity,
            Animals = new(),
            IsTrial = (bool)order.IsTrial,
            SitterId = order.Sitter.Id,
            Status = order.Status,
            Type = order.Type
        };


        _ordersServiceMock
            .Setup(x => x.GetOrderById(id))
            .Returns(order);

        //when
        var actual = _sut.GetOrderById(id);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult!.Value as OrderResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedOrder.Status, actualValue.Status);
        Assert.AreEqual(expectedOrder.Type, actualValue.Type);
        Assert.AreEqual(expectedOrder.IsTrial, actualValue.IsTrial);
        Assert.AreEqual(expectedOrder.ClientId, actualValue.ClientId);
        Assert.AreEqual(expectedOrder.SitterId, actualValue.SitterId);
        Assert.AreEqual(expectedId, actualValue.Id);

        _ordersServiceMock.Verify(x => x.GetOrderById(id), Times.Once);
    }

    [Test]
    public void GetAllOrders_WhenValidRequestPassed_ThenStatusCodeOk()
    {
        //given
        var orders = Orders();

        var expectedOrders = ExpectedOrders(orders);

        _ordersServiceMock
            .Setup(x => x.GetAllOrders())
            .Returns(orders);

        //when
        var actual = _sut.GetAllOrders();

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value as List<OrderResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.IsTrue(actualValue is not null);
        Assert.AreEqual(expectedOrders.Count, actualValue.Count);
        _ordersServiceMock.Verify(x => x.GetAllOrders(), Times.Once);
    }

    public void GetCommentsByOrderId_WhenValidRequestPassed_ThenThrow200OK()
    {
        //given
        var expectedComments = ExpectedComments();
        var commentsToGet = CommentsToGet();
        var targetOrder = new Order()
        {
            Id = 1,
            Comments = new(),
            Status = Status.Finished,
            WalkQuantity = 4,
            Sitter =new(),
            Animals = new(),
            Client = new(),
            IsTrial = false,
            Type = Service.Walk
        };

        _ordersServiceMock
            .Setup(x => x.GetCommentsByOrderId(targetOrder.Id))
            .Returns(commentsToGet);

        //when
        var actual = _sut.GetCommentsByOrderId(targetOrder.Id);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value as List<CommentResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.IsTrue(actualValue is not null);
        Assert.AreEqual(expectedComments.Count, actualValue.Count);
        _ordersServiceMock.Verify(x => x.GetCommentsByOrderId(targetOrder.Id), Times.Once);
    }

    [Test]
    public void AddCommentToOrder_WhenValidRequestPassed_ThenThrow200OK()
    {
        //given
        var commentToAdd = new CommentRequest() { Rating = 3, Text = "blah blah" };
        var expectedId = 4;
        var id = 2;

        _ordersServiceMock
            .Setup(x => x.AddCommentToOrder(id, It.IsAny<Comment>()))
            .Returns(expectedId);

        //when
        var actual = _sut.AddCommentToOrder(id, commentToAdd);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.IsTrue(actualValue is not null);
        Assert.AreEqual(expectedId, actualValue);

        _ordersServiceMock.Verify(x => x.AddCommentToOrder(
            id, It.Is<Comment>(
            c => c.Rating == commentToAdd.Rating &&
            c.Text == commentToAdd.Text)
            ), Times.Once);
    }

    [Test]
    public void DeleteOrderById_WhenValidRequestPassed_ThenThrowNoContent()
    {
        //given
        var id = 32;

        //when
        var actual = _sut.DeleteOrderById(id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _ordersServiceMock.Verify(x => x.DeleteOrderById(id), Times.Once);
    }

    [Test]
    public void UpdateOrder_WhenCorrectParamsPassed_ReturnNoContent()
    {
        //given
        var id = 2;
        var PropertiesToChange = new UpdateOrderOverexposeRequest()
        {
            AnimalIds = new() { new()},
            District = Data.Enums.District.All,
            WorkDate = DateTime.Now
        };

        //when
        var actual = _sut.UpdateOrder(PropertiesToChange, id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _ordersServiceMock.Verify(x => x.UpdateOrder(
            It.Is<UpdateOrderModel>(
                p => p.District == PropertiesToChange.District &&
                p.WorkDate == PropertiesToChange.WorkDate), 
            id), Times.Once);
    }

    private List<Order>  Orders() => new List<Order>()
        {
            new Order
            {
                Id = 3,
                Animals = new(),
                Client = new() { Id = 3 },
                Status = Status.Finished,
                Type = Service.Walk,
                IsTrial = false,
                Sitter = new() { Id = 2 }
            },

            new Order
            {
                Id = 2,
                Animals = new(),
                Client = new() { Id = 1 },
                Status = Status.Created,
                Type = Service.Walk,
                IsTrial = false,
                Sitter = new() { Id = 3 }
            },

            new Order
            {
                Id = 1,
                Animals = new(),
                Client = new() { Id = 2 },
                Status = Status.InProgress,
                Type = Service.Walk,
                IsTrial = false,
                Sitter = new() { Id = 1 }
            },
        };

    private List<OrderResponse> ExpectedOrders(List<Order> orders) => new List<OrderResponse>()
            {
            new OrderResponse
            {
                Id = 3,
                Animals = new(),
                ClientId = orders[0].Client.Id,
                Status = Status.Finished,
                Type = Service.Walk,
                IsTrial = false,
                SitterId = orders[0].Sitter.Id
            },

            new OrderResponse
            {
                Id = 2,
                Animals = new(),
                ClientId = orders[1].Client.Id,
                Status = Status.Created,
                Type = Service.Walk,
                IsTrial = false,
                SitterId = orders[1].Sitter.Id
            },

            new OrderResponse
            {
                Id = 1,
                Animals = new(),
                ClientId = orders[2].Client.Id,
                Status = Status.InProgress,
                Type = Service.Walk,
                IsTrial = false,
                SitterId = orders[2].Sitter.Id
            },
        };

    private List<CommentResponse> ExpectedComments() => new List<CommentResponse>()
    {
        new(){Id = 1, OrderId = 1, Rating = 3, Text = "blah blah"},
        new(){Id = 2, OrderId = 2, Rating = 5, Text = "Pudge is here"},
        new(){Id = 3, OrderId = 3, Rating = 1, Text = "he he he he he"}
    };

    private List<Comment> CommentsToGet() => new List<Comment>()
    {
        new(){Id = 1, Order = new Order(){ Id = 1 }, Rating = 3, Text = "blah blah"},
        new(){Id = 2, Order = new Order(){ Id = 2 }, Rating = 5, Text = "Pudge is here"},
        new(){Id = 3, Order = new Order(){ Id = 3 }, Rating = 1, Text = "he he he he he"}
    };
}
