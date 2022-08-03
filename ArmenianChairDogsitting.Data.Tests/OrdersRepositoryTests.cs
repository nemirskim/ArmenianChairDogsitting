using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Tests;

public class OrderRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private OrdersRepository _sut;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
        .UseInMemoryDatabase(databaseName: $"TestDb")
        .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();
        _sut = new OrdersRepository(_context);

        _context.Orders.Add(new OrderWalk()
        {
            WalkQuantity = 3,
            IsTrial = true,
            Status = Status.Created,
            Type = ServiceEnum.Walk,
            Animals = new List<Animal>(),
            Client = new() { Name = "Zhora", LastName = "Zhora", Email = "ugabuga@kek.com", Password = " monkeySleep" },
        });

        _context.Orders.Add(new OrderDailySitting()
        {
            WalkQuantity = 3,
            Status = Status.Finished,
            Type = ServiceEnum.DailySitting,
            DayQuantity = 2,
            Animals = new List<Animal>(),
            Client = new() { Name = "Zhora", LastName = "Zhora", Email = "ugabuga@kek.com", Password = " monkeySleep" },
            Comments = new()
        }); ;

        _context.Orders.Add(new OrderOverexpose()
        {
            Status = Status.Created,
            Type = ServiceEnum.Overexpose,
            DayQuantity = 3,
            WalkPerDayQuantity = 3,
            Animals = new(),
            Comments = new(),
            Client = new() { Name = "Grisha", LastName = "Grisha", Email = "ugabuga@kek.com", Password = " monkeySleep" },
        });

        _context.SaveChanges();

        _context.Orders.Add(new OrderOverexpose()
        {
            Status = Status.Created,
            Type = ServiceEnum.Overexpose,
            DayQuantity = 3,
            WalkPerDayQuantity = 3,
            Animals = new(),
            Comments = new(),
            Client = new() { Name = "Grisha", LastName = "Grisha", Email = "ugabuga@kek.com", Password = " monkeySleep" },
            IsDeleted = true
        });

        _context.SaveChanges();
    }

    [Test]
    public void GetAllOrders_WhenCalled_ReturnsAllOrders()
    {
        //given
        var expectedCount = 3;

        //when
        var returnedOrders = _sut.GetAllOrders();

        //then
        Assert.AreEqual(3, returnedOrders.Count);
    }

    [Test]
    public void GetOrderById_WhenValidTitlePassed_ThenReturnOrder()
    {
        //given
        var expectedOrder = new OrderOverexpose()
        {
            Id = 3,
            Status = Status.Created,
            Type = ServiceEnum.Overexpose,
            DayQuantity = 3,
            WalkPerDayQuantity = 3,
            Animals = new(),
            Comments = new(),
            Client = new() { Id = 3, Name = "Grisha", Email = "ugabuga@kek.com", Password = " monkeySleep" },
        };

        //when
        var actualOrder = _sut.GetOrderById(3);

        //then
        Assert.AreEqual(expectedOrder.Id, actualOrder.Id);
        Assert.AreEqual(expectedOrder.Client.Id, actualOrder.Client.Id);
        Assert.AreEqual(expectedOrder.Animals.Count, actualOrder.Animals.Count);
        Assert.AreEqual(expectedOrder.Comments.Count, actualOrder.Comments.Count);
    }

    [Test]
    public void UpdateOrderStatus_WhenValidTitlePassed_ThenUpdateStatus()
    {
        //given 
        Status expectedStatus = Status.InProgress;
        var orderId = 1;

        //when
        _sut.UpdateOrderStatus(Status.InProgress, orderId);

        //then
        var changedOrder = _sut.GetOrderById(orderId);
        var actualStatus = changedOrder!.Status;

        Assert.IsTrue(expectedStatus == actualStatus);
    }

    [Test]
    public void AddCommentToOrder_WhenParamsIsValid_ThenReturnIdOfNewComment()
    {
        //given
        var expectedChangesInOrder = ExpectedChangesInOrder();
        var commentToAdd = CommentToAdd();
        var orderIdToChange = 3;
        var expectedId = 1;

        //when
        var actualId = _sut.AddCommentToOrder(orderIdToChange, commentToAdd);

        //then
        var actualOrder = _sut.GetOrderById(orderIdToChange);

        Assert.AreEqual(expectedChangesInOrder.Id, actualOrder.Id);
        Assert.AreEqual(expectedChangesInOrder.Comments.Count, actualOrder.Comments.Count);
        Assert.AreEqual(expectedId, actualId);
        Assert.AreEqual(expectedChangesInOrder.Comments[0].Text, actualOrder.Comments[0].Text);
    }

    [Test]
    public void DeleteOrderById_WhenCorrectIdAndStatusPassed_ThenDeleteOrder()
    {
        //given 
        var id = 1;
        var comment = _context.Orders.FirstOrDefault(c => c.Id == id);
        var expectedCount = 2;

        //When
        _sut.DeleteOrderById(id);

        //then
        var orders = _sut.GetAllOrders();
        Assert.AreEqual(expectedCount, orders.Count);
    }

    private OrderOverexpose ExpectedChangesInOrder() =>
        new OrderOverexpose()
        {
            Id = 3,
            Status = Status.Created,
            Type = ServiceEnum.Overexpose,
            DayQuantity = 3,
            WalkPerDayQuantity = 3,
            Animals = new(),
            Comments = new() { new() { Id = 4, Text = "blah blah" } },
            Client = new() { Id = 3, Name = "Grisha", Email = "ugabuga@kek.com", Password = " monkeySleep" },
        };

    private Comment CommentToAdd() => new() { Text = "blah blah" };
}