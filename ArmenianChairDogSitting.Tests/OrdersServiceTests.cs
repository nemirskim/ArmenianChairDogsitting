using ArmenianChairDogsitting.Business;
using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class OrdersServiceTests
{
    private Mock<IOrdersRepository> _ordersRepository;
    private Mock<ISittersRepository> _sittersRepository;
    private Mock<IClientsRepository> _clientsRepository;
    private Mock<IPromocodesService> _promocodesRepository;
    private OrdersService _sut;

    [SetUp]
    public void Setup()
    {
        _ordersRepository = new Mock<IOrdersRepository>();
        _sittersRepository = new Mock<ISittersRepository>();
        _clientsRepository = new Mock<IClientsRepository>();
        _sut = new OrdersService(
            _ordersRepository.Object,
            _clientsRepository.Object,
            _sittersRepository.Object,
            _promocodesRepository.Object);
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
        _ordersRepository.Verify(x => x.AddOrder(
            It.Is<Order>(
                o => o.IsTrial == orderToAdd.IsTrial &&
                o.WalkQuantity == orderToAdd.WalkQuantity &&
                o.Status == orderToAdd.Status &&
                o.Animals.Count == orderToAdd.Animals.Count &&
                o.Comments.Count == orderToAdd.Comments.Count &&
                o.Id == orderToAdd.Id)
            ), Times.Once);
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
            .Setup(x => x.GetOrderById(orderID))
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
        var status = Status.InProgress;
        _ordersRepository
            .Setup(x => x.GetOrderById(orderId))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.UpdateOrderStatus(status, orderId));

        //when
        _sut.UpdateOrderStatus(Status.InProgress, orderId);

        //then
        _ordersRepository.Verify(x => x.UpdateOrderStatus(status, orderId), Times.Once);
    }

    [Test]
    public void UpdateOrderStatus_WhenOrderDoesntExist_ThenThrowNotFoundException()
    {
        //given
        Order orderFromRepo = null;

        var orderId = 34;

        _ordersRepository
            .Setup(x => x.GetOrderById(orderId))
            .Returns(orderFromRepo);

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
            .Setup(x => x.GetOrderById(orderId))
            .Returns(orderFromRepo[1]);

        _ordersRepository
            .Setup(x => x.AddCommentToOrder(orderId, It.IsAny<Comment>()))
            .Returns(orderFromRepo[1].Comments[0].Id);

        //when
        var actualId = _sut.AddCommentToOrder(orderId, commentToAdd);

        //then
        Assert.AreEqual(expectedId, actualId);
        _ordersRepository.Verify(x => x.GetOrderById(orderId), Times.Once);

        _ordersRepository.Verify(x => x.AddCommentToOrder(
            orderId, It.Is<Comment>(
                c => c.Id == commentToAdd.Id &&
                c.Text == commentToAdd.Text)
            ), Times.Once);
    }

    [Test]
    public void AddCommentToOrder_WhenOrderDoesntExist_ThenThrowNotFoundException()
    {
        //given
        Order orderFromRepo = null;
        var someComment = new Comment();

        var orderId = 555;

        _ordersRepository
            .Setup(x => x.GetOrderById(orderId))
            .Returns(orderFromRepo);

        _ordersRepository
            .Setup(x => x.AddCommentToOrder(orderId, It.IsAny<Comment>()));

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
            .Setup(x => x.GetOrderById(id))
            .Returns(new Order() { Id = id, Status=Status.Created, IsDeleted = false });

        //when
        _sut.DeleteOrderById(id);

        //then
        _ordersRepository.Verify(x => x.GetOrderById(id), Times.Once);
        _ordersRepository.Verify(x => x.DeleteOrderById(id), Times.Once);
    }

    [Test]
    public void DeleteOrderById_WhenCorrectIdIsNotFound_ThrowNotFounddException()
    {
        //given
        var id = 2;
        Order order = null!;

        _ordersRepository
            .Setup(x => x.GetOrderById(id))
            .Returns(order!);

        //when then

        Assert.Throws<NotFoundException>(() => _sut.DeleteOrderById(id));
        _ordersRepository.Verify(x => x.GetOrderById(id), Times.Once);
        _ordersRepository.Verify(x => x.DeleteOrderById(id), Times.Never);
    }

    [Test]
    public void DeleteOrderById_WhenStatusIsInProgress_ThrowNotFounddException()
    {
        //given
        var id = 2;

        _ordersRepository
            .Setup(x => x.GetOrderById(id))
            .Returns(new Order() { Id = id, Status = Status.InProgress, IsDeleted = false });

        //when then

        Assert.Throws<ForbiddenException>(() => _sut.DeleteOrderById(id));
        _ordersRepository.Verify(x => x.GetOrderById(id), Times.Once);
        _ordersRepository.Verify(x => x.DeleteOrderById(id), Times.Never);
    }

    [Test]
    public void UpdateOrder_WhenIdIsInvalid_ThenThrowNotFoundException()
    {
        //given
        var id = 2;
        Order order = null!;
        _ordersRepository
            .Setup(x => x.GetOrderById(id))
            .Returns(order!);

        //when then

        Assert.Throws<NotFoundException>(() => _sut.UpdateOrder(It.IsAny<UpdateOrderModel>(), id));
        _ordersRepository.Verify(x => x.GetOrderById(id), Times.Once);
        _ordersRepository.Verify(x => x.ChangeOrder(It.IsAny<UpdateOrderModel>(),id), Times.Never);
    }

    [Test]
    public void UpdateOrder_WhenStatusInProgress_ThenThrowForbidden()
    {
        //given
        var id = 2;

        _ordersRepository
            .Setup(x => x.GetOrderById(id))
            .Returns(new Order() { Id = id, Status = Status.InProgress, IsDeleted = false });

        //when then

        Assert.Throws<ForbiddenException>(() => _sut.UpdateOrder(It.IsAny<UpdateOrderModel>(), id));
        _ordersRepository.Verify(x => x.GetOrderById(id), Times.Once);
        _ordersRepository.Verify(x => x.ChangeOrder(It.IsAny<UpdateOrderModel>(), id), Times.Never);
    }

    [Test]
    public void GetCommentsByOrderId_WhenIdIsInvlid_ThenThrowNotFoundException()
    {
        //given
        var id = 2;
        Order order = null!;
        _ordersRepository
            .Setup(x => x.GetOrderById(id))
            .Returns(order!);

        //when then

        Assert.Throws<NotFoundException>(() => _sut.GetCommentsByOrderId(id));
        _ordersRepository.Verify(x => x.GetOrderById(id), Times.Once);
        _ordersRepository.Verify(x => x.GetCommentsByOrderId( id), Times.Never);
    }

    private List<Order> SetOrders()
    {
        return new List<Order>()
        {
            new Order()
            {
                Id = 34,
                Animals = new(),
                Client = new(),
                WalkQuantity = 2,
                IsTrial = true,
                Sitter = new(),
                Status = Status.Created,
                Type = Service.Walk
            },
            new Order()
            {
                Id = 42,
                Animals = new(),
                Client = new(),
                WalkQuantity = 2,
                IsTrial = true,
                Sitter = new(),
                Status = Status.Created,
                Type = Service.Walk,
                Comments = new List<Comment> { new Comment() { Id = 4, Text = "blah blah" }}
    },
            new Order()
            {
                Id = 76,
                Animals = new(),
                Client = new(),
                WalkQuantity = 2,
                IsTrial = true,
                Sitter = new(),
                Status = Status.Created,
                Type = Service.Walk
            }
        };
    }

    private Order OrderFromRepo() =>
        new Order()
        {
            Id = 34,
            Animals = new(),
            Client = new() { Id = 1 },
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new() { Id = 1 },
            Status = Status.Created,
            Type = Service.Walk
        };

    private Order ExpectedOrder() =>
        new Order()
        { 
            Id = 34,
            Animals = new(),
            Client = new() { Id = 1 },
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new() { Id = 1 },
            Status = Status.Created,
            Type = Service.Walk
        };
    

    private Order OrderToAdd() =>
        new Order()
        {
            Id = 34,
            Animals = new(),
            Client = new() { Id = 1},
            WalkQuantity = 2,
            IsTrial = true,
            Sitter = new() { Id = 1 },
            Status = Status.Created,
            Type = Service.Walk,
            Comments = new()
        };

}
