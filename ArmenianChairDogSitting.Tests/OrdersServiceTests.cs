using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogSitting.Business.Tests;

public class OrdersServiceTests
{
    private Mock<IOrdersRepository> _ordersRepository;
    private OrdersService _sut;

    [SetUp]
    public void Setup()
    {
        _ordersRepository = new Mock<IOrdersRepository>();
        _sut = new OrdersService(_ordersRepository.Object);
    }

    [Test]
    public void AddOrder_WhenCalled_ThenReturnIdOfAddedOrder()
    {
        //given
        var orderToAdd = new OrderWalk()
        {
            Id = 34,
            Animals = new(),
            Client = new(),
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new(),
            Status = Status.Created,
            Type = ServiceEnum.Walk
        };

        var expectedId = 34;

        _ordersRepository
            .Setup(x => x.AddOrder(It.IsAny<Order>()))
            .Returns(expectedId);

        //when
        var returnedInt = _sut.AddOrder(orderToAdd);

        //then
        Assert.AreEqual(expectedId, returnedInt);
    }

    [Test]
    public void GetAllOrders_WhenExist_ThenReturnOrders()
    {
        //given
        var ordersFromRepo = SetOrders();

        _ordersRepository
            .Setup(x => x.GetAllOrders())
            .Returns(ordersFromRepo);

        //when
        var actual = _sut.GetAllOrders();

        //then
        Assert.IsTrue(actual is not null);
        Assert.IsTrue(actual!.Count == 3);
        Assert.IsTrue(actual is List<Order>);
        Assert.IsTrue(actual[0].Client is Client);
        Assert.IsTrue(actual[0].Sitter is Sitter);
        Assert.IsTrue(actual[0].Animals is List<Animal>);
    }

    [Test]
    public void GetOrderById_WhenOrderExist_ReturnOrder()
    {
        //given
        var expectedOrder = new OrderWalk()
        {
            Id = 34,
            Animals = new(),
            Client = new() { Id = 1 },
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new() { Id = 1 },
            Status = Status.Created,
            Type = ServiceEnum.Walk
        };

        Order orderFromRepo = new OrderWalk()
        {
            Id = 34,
            Animals = new(),
            Client = new() { Id = 1},
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new() { Id = 1 },
            Status = Status.Created,
            Type = ServiceEnum.Walk
        };

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        //when
        var actualOrder = _sut.GetOrderById(34);

        //then
        Assert.AreEqual(actualOrder.Id, expectedOrder.Id);
        Assert.AreEqual(actualOrder.Status, expectedOrder.Status);
        Assert.AreEqual(actualOrder.Type, expectedOrder.Type);
        Assert.AreEqual(actualOrder.Animals.Count, expectedOrder.Animals.Count);
        Assert.AreEqual(actualOrder.Sitter.Id, expectedOrder.Sitter.Id);
        Assert.AreEqual(actualOrder.Client.Id, expectedOrder.Client.Id);
        _ordersRepository.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetOrderById_WhenOrderDoesntExist_ReturnOrder()
    {
        //given
        Order orderFromRepo = null;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        //when then
        Assert.Throws<NotFoundException>(() => _sut.GetOrderById(34));
        _ordersRepository.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void UpdateOrderStatus_WhenOrderExist_KeepWorking()
    {
        //given
        Order orderFromRepo = new OrderWalk()
        {
            Id = 34,
            Animals = new(),
            Client = new() { Id = 1 },
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new() { Id = 1 },
            Status = Status.Created,
            Type = ServiceEnum.Walk
        };

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()));

        //when
        _sut.UpdateOrderStatus(Status.InProgress, 34);

        //then
        _ordersRepository.Verify(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void UpdateOrderStatus_WhenOrderDoesntExist_ThenThrowNotFoundException()
    {
        //given
        Order orderFromRepo = null;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()));

        //when then
        Assert.Throws<NotFoundException>(() => _sut.UpdateOrderStatus(Status.InProgress, 34));
        _ordersRepository.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
        _ordersRepository.Verify(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()), Times.Never);
    }

    private List<Order> SetOrders()
    {
        return new List<Order>()
        {
            new OrderWalk()
            {
                Id = 34,
                Animals = new(),
                Client = new(),
                WalkQuantity = 2,
                IsTrial = true,
                Sitter = new(),
                Status = Status.Created,
                Type = ServiceEnum.Walk
            },
            new OrderWalk()
            {
                Id = 42,
                Animals = new(),
                Client = new(),
                WalkQuantity = 2,
                IsTrial = true,
                Sitter = new(),
                Status = Status.Created,
                Type = ServiceEnum.Walk
            },
            new OrderWalk()
            {
                Id = 76,
                Animals = new(),
                Client = new(),
                WalkQuantity = 2,
                IsTrial = true,
                Sitter = new(),
                Status = Status.Created,
                Type = ServiceEnum.Walk
            }
        };
    }
}
