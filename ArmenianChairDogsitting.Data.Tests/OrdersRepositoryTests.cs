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
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _sut = new OrdersRepository(_context);

        _context.Orders.Add(new OrderWalk()
        {
            Id = 1,
            WalkQuantity = 3,
            IsTrial = true,
            Status = Status.Created,
            Type = Service.WalkOneHour
        });

        _context.Orders.Add(new OrderDailySitting()
        {
            Id = 2,
            WalkQuantity = 3,
            Status = Status.Finished,
            Type = Service.WalkOneHour,
            DayQuantity = 2            
        });

        _context.Orders.Add(new OrderOverexpose()
        {
            Id = 3,
            Status = Status.Created,
            Type = Service.WalkOneHour,
            DayQuantity = 3,
            WalkPerDayQuantity = 3
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
            Type = Service.WalkOneHour,
            DayQuantity = 3,
            WalkPerDayQuantity = 3
        };

        //when
        var actualOrder = _sut.GetOrderById(3);

        //then
        Assert.AreEqual(expectedOrder.Id, actualOrder.Id);
    }

    [Test]
    public void UpdateOrderStatus_WhenValidTitlePassed_ThenUpdateStatus()
    {
        //given 
        Status expectedStatus = Status.InProgress;

        //when
        _sut.UpdateOrderStatus(Status.InProgress, 1);

        //then
        Assert.IsTrue(expectedStatus == Status.InProgress);
    }
}