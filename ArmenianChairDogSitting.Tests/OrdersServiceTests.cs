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
        var orderToAdd = OrderToAdd();

        var expectedId = 34;

        _ordersRepository
            .Setup(x => x.AddOrder(It.IsAny<Order>()))
            .Returns(expectedId);

        //when
        var returnedInt = _sut.AddOrder(orderToAdd);

        //then
        Assert.AreEqual(expectedId, returnedInt);
        _ordersRepository.Verify(x => x.AddOrder(orderToAdd), Times.Once);
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
        Assert.IsTrue(actual!.Count == ordersFromRepo.Count);
        Assert.IsTrue(actual is List<Order>);
        Assert.IsTrue(actual[0].Client is Client);
        Assert.IsTrue(actual[0].Sitter is Sitter);
        Assert.IsTrue(actual[0].Animals is List<Animal>);
        _ordersRepository.Verify(x => x.GetAllOrders(), Times.Once);
    }

    [Test]
    public void GetOrderById_WhenOrderExist_ReturnOrder()
    {
        //given
        var expectedOrder = ExpectedOrder();

        Order orderFromRepo = OrderFromRepo();

        var orderID = 34;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        //when
        var actualOrder = _sut.GetOrderById(orderID);

        //then
        Assert.AreEqual(actualOrder.Id, expectedOrder.Id);
        Assert.AreEqual(actualOrder.Status, expectedOrder.Status);
        Assert.AreEqual(actualOrder.Type, expectedOrder.Type);
        Assert.AreEqual(actualOrder.Animals.Count, expectedOrder.Animals.Count);
        Assert.AreEqual(actualOrder.Sitter.Id, expectedOrder.Sitter.Id);
        Assert.AreEqual(actualOrder.Client.Id, expectedOrder.Client.Id);
        _ordersRepository.Verify(x => x.GetOrderById(orderID), Times.Once);
    }

    [Test]
    public void UpdateOrderStatus_WhenOrderExist_KeepWorking()
    {
        //given
        Order orderFromRepo = OrderFromRepo();

        var orderId = 34;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()));

        //when
        _sut.UpdateOrderStatus(Status.InProgress, orderId);

        //then
        _ordersRepository.Verify(x => x.UpdateOrderStatus(Status.InProgress, orderId), Times.Once);
    }

    [Test]
    public void UpdateOrderStatus_WhenOrderDoesntExist_ThenThrowNotFoundException()
    {
        //given
        Order orderFromRepo = null;

        var orderId = 34;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.UpdateOrderStatus(It.IsAny<Status>(), It.IsAny<int>()));

        //when then
        Assert.Throws<NotFoundException>(() => _sut.UpdateOrderStatus(Status.InProgress, orderId));
        _ordersRepository.Verify(x => x.GetOrderById(orderId), Times.Once);
        _ordersRepository.Verify(x => x.UpdateOrderStatus(Status.InProgress, orderId), Times.Never);
    }

    [Test]
    public void AddCommentToOrder_WhenOrderExist_ThenReturnIdOfNewComment()
    {
        //given
        var orderFromRepo = SetOrders();
        var commentToAdd = new Comment() { Id = 4, Text = "blah blah" };
        var expectedId = 4;

        var orderId = 42;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo[1]);

        _ordersRepository
            .Setup(x => x.AddCommentToOrder(It.IsAny<int>(), It.IsAny<Comment>()))
            .Returns(orderFromRepo[1].Comments[0].Id);

        //when
        var actualId = _sut.AddCommentToOrder(orderId, commentToAdd);

        //then
        Assert.AreEqual(expectedId, actualId);
        _ordersRepository.Verify(x => x.GetOrderById(orderId), Times.Once);
        _ordersRepository.Verify(x => x.AddCommentToOrder(orderId, commentToAdd), Times.Once);
    }

    [Test]
    public void AddCommentToOrder_WhenOrderDoesntExist_ThenThrowNotFoundException()
    {
        //given
        Order orderFromRepo = null;
        var someComment = new Comment();

        var orderId = 555;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.AddCommentToOrder(It.IsAny<int>(), It.IsAny<Comment>()));

        //when then
        Assert.Throws<NotFoundException>(() => _sut.AddCommentToOrder(orderId, someComment));
        _ordersRepository.Verify(x => x.GetOrderById(orderId), Times.Once);
        _ordersRepository.Verify(x => x.AddCommentToOrder(orderId, It.IsAny<Comment>()), Times.Never);
    }

    [Test]
    public void DeleteOrderById_WhenCorrectIdAndStatusPassed_KeepWorking()
    {
        //given
        var id = 2;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(new OrderWalk() { Id = id, Status=Status.Created, IsDeleted = false });

        //when
        _sut.DeleteOrderById(id);

        //then
        _ordersRepository.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
        _ordersRepository.Verify(x => x.DeleteOrderById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void DeleteOrderById_WhenCorrectIdIsNotFound_ThrowNotFounddException()
    {
        //given
        var id = 2;
        Order order = null!;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(order!);

        //when then

        Assert.Throws<NotFoundException>(() => _sut.DeleteOrderById(id));
        _ordersRepository.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
        _ordersRepository.Verify(x => x.DeleteOrderById(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void DeleteOrderById_WhenStatusIsInProgress_ThrowNotFounddException()
    {
        //given
        var id = 2;

        _ordersRepository
            .Setup(x => x.GetOrderById(It.IsAny<int>()))
            .Returns(new OrderWalk() { Id = id, Status = Status.InProgress, IsDeleted = false });

        //when then

        Assert.Throws<ForbiddenException>(() => _sut.DeleteOrderById(id));
        _ordersRepository.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
        _ordersRepository.Verify(x => x.DeleteOrderById(It.IsAny<int>()), Times.Never);
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
                Type = ServiceEnum.Walk,
                Comments = new List<Comment> { new Comment() { Id = 4, Text = "blah blah" }}
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

    private Order OrderFromRepo()
    {
        return new OrderWalk()
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
    }

    private OrderWalk ExpectedOrder()
    {
        return new OrderWalk()
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
    }

    private OrderWalk OrderToAdd()
    {
        return new OrderWalk()
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
    }
}
