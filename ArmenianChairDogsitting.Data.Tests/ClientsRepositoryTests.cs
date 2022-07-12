using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Tests;

public class ClientsRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private ClientsRepository _sut;

    [SetUp]
    public void SetUp()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb")
            .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();
        _sut = new ClientsRepository(_context);

        _context.Clients.Add(new Client()
        {
            Id = 1,
            Name = "Marina",
            LastName = "Alekseeva",
            Dogs = new()
                { new() { Id = 1, Name = "Bob" }},
            IsDeleted = false
            //Orders = new Order() { Id = 1, Type = new DailySitting() },       
        });

        _context.SaveChanges();
    }

    [Test]
    public void RemoveClientTest_WhenCorrectIdIsPassed_ThenSoftDeleteApplied()
    {
        //given
        var id = 1;
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);

        //when
        _sut.RemoveOrRestoreClient(id);

        //then
        Assert.True(client!.IsDeleted);
    }


}
