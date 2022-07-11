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
        Random random = new Random();

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
            Type = Service.Walk,
            Animals = new List<Animal>(),
            Client = new() { Name = "Zhora"}            
        });

        _context.Orders.Add(new OrderDailySitting()
        {
            WalkQuantity = 3,
            Status = Status.Finished,
            Type = Service.DailySitting,
            DayQuantity = 2,
            Animals = new List<Animal>(),
            Client = new() { Name = "Zhora" },
            Comments = new()
        }); ;

        _context.Orders.Add(new OrderOverexpose()
        {
            Status = Status.Created,
            Type = Service.Overexpose,
            DayQuantity = 3,
            WalkPerDayQuantity = 3,
            Animals = new(),
            Comments = new(),
            Client = new() { Name = "Grisha"},
            Sitter = new() { Name = "Antosha"}
        });

        _context.SaveChanges();
    }

    [Test]
    public void GetAllOrders_WhenCalled_ReturnsAllOrders()
    {
        //given in SetUp

        //when
        var returnedOrders = _sut.GetAllOrders();

        //then
        Assert.AreEqual(3, returnedOrders.Count());
    }

    [Test]
    public void GetOrderById_WhenValidTitlePassed_ThenReturnOrder()
    {
        //given
        var expectedOrder = new OrderOverexpose()
        {
            Id = 3,
            Status = Status.Created,
            Type = Service.Overexpose,
            DayQuantity = 3,
            WalkPerDayQuantity = 3,
            Animals = new(),
            Comments = new(),
            Client = new() { Id = 3, Name = "Grisha" },
            Sitter = new() { Id = 1, Name = "Antosha" }
        };

        //when
        var actualOrder = _sut.GetOrderById(3);

        //then
        Assert.AreEqual(expectedOrder.Id, actualOrder.Id);
        Assert.AreEqual(expectedOrder.Sitter.Id, actualOrder.Sitter.Id);
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
}