using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
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

        _ordersServiceMock.Setup(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()));

        //when
        var result = _sut.ChangeOrderStatus(statusToParam, idToParam);

        //then
        var actualResult = result as NoContentResult;
        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _ordersServiceMock.Verify(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()), Times.Once);
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
        var orders = Orders();

        var expectedOrders = ExpectedOrders(orders);

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
        _ordersServiceMock.Verify(x => x.GetAllOrders(), Times.Once);
    }

    public void GetCommentsByOrderId_WhenValidRequestPassed_ThenThrow200OK()
    {
        //given
        var expectedComments = ExpectedComments();
        var commentsToGet = CommentsToGet();
        var targetOrder = new OrderWalk()
        {
            Id = 1,
            Comments = new(),
            Status = Status.Finished,
            WalkQuantity = 4,
            Sitter =new(),
            Animals = new(),
            Client = new(),
            IsTrial = false,
            Type = ServiceEnum.Walk
        };

        _ordersServiceMock
            .Setup(x => x.GetCommentsByOrderId(It.IsAny<int>()))
            .Returns(commentsToGet);

        //when
        var actual = _sut.GetCommentsByOrderId(targetOrder.Id);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value as List<CommentResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.IsTrue(actualValue is not null);
        Assert.AreEqual(expectedComments.Count, actualValue.Count);
        _ordersServiceMock.Verify(x => x.GetCommentsByOrderId(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void AddCommentToOrder_WhenValidRequestPassed_ThenThrow200OK()
    {
        //given
        var commentToAdd = new CommentRequest() { Rating = 3, Text = "blah blah" };
        var expectedId = 4;

        _ordersServiceMock
            .Setup(x => x.AddCommentToOrder(It.IsAny<int>(), It.IsAny<Comment>()))
            .Returns(expectedId);

        //when
        var actual = _sut.AddCommentToOrder(2, commentToAdd);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.IsTrue(actualValue is not null);
        Assert.AreEqual(expectedId, actualValue);
        _ordersServiceMock.Verify(x => x.AddCommentToOrder(It.IsAny<int>(), It.IsAny<Comment>()), Times.Once);
    }

    private List<Order>  Orders() => new List<Order>()
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

    private List<AbstractOrderResponse> ExpectedOrders(List<Order> orders) => new List<AbstractOrderResponse>()
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

    private List<CommentResponse> ExpectedComments() => new List<CommentResponse>()
    {
        new(){Id = 1, OrderId = 1, Rating = 3, Text = "blah blah"},
        new(){Id = 2, OrderId = 2, Rating = 5, Text = "Pudge is here"},
        new(){Id = 3, OrderId = 3, Rating = 1, Text = "he he he he he"}
    };

    private List<Comment> CommentsToGet() => new List<Comment>()
    {
        new(){Id = 1, Order = new OrderWalk(){ Id = 1 }, Rating = 3, Text = "blah blah"},
        new(){Id = 2, Order = new OrderWalk(){ Id = 2 }, Rating = 5, Text = "Pudge is here"},
        new(){Id = 3, Order = new OrderWalk(){ Id = 3 }, Rating = 1, Text = "he he he he he"}
    };
}
