using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Data.Tests;

public class Tests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}