using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
        });

        _context.Clients.Add(new Client()
        {
            Id = 2,
            Name = "Konstantin",
            LastName = "Zasnul",
            Dogs = new()
                { new() { Id = 65, Name = "Persik" }},
            IsDeleted = true 
        });

        _context.SaveChanges();
    }

    [Test]
    public void AddClientTest_WhenNewClientIsCorrect_ThenShouldAddExpectedClient()
    {
        //given 
        var client = new Client()
        {
            Id = 3,
            Name = "Valery",
            LastName = "Meladze"
        };

        //when 
        _sut.AddClient(client);

        //then 
        var expected = _context.Clients.FirstOrDefault(c => c.Id == client.Id);
        Assert.AreEqual(expected.Id, client.Id);
    }

    [Test]
    public void GetClientByIdTest_WhenIdIsCorrect_ShouldReturnExpectedClient()
    {
        //given in setup 

        //when 
        var client = _sut.GetClientById(2);

        //then 
        Assert.AreEqual(2, client.Id);
        Assert.NotNull(client);
    }

    [Test]
    public void GetAllClientsTest_ShouldReturnAllOfExpectedClients()
    {
        //given 
        var expectedClientsQuantity = 1;

        //when 
        var clients = _sut.GetAllClients();

        //then 
        Assert.AreEqual(expectedClientsQuantity, clients.Count);
    }

    [Test]
    public void UpdateClientTest_WhenCorrectIdIsPassed_ThenUpdateClientProfileApplied()
    {
        //given 
        var id = 2;
        var actual = _context.Clients.FirstOrDefault(c => c.Id == id);
        actual!.LastName = "Prosnulsya";
        _context.SaveChanges();

        //when
        _sut.UpdateClient(actual);

        //then 
        Assert.AreEqual(id, actual.Id);
        Assert.AreEqual("Prosnulsya", actual.LastName);
    }

    [Test]
    public void RemoveClientTest_WhenCorrectIdIsPassed_ThenSoftDeleteApplied()
    {
        //given 
        var id = 1;
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        client.IsDeleted = true;

        //when 
        _sut.RemoveOrRestoreClient(client);

        var actual = _context.Clients.FirstOrDefault(c => c.Id == id);
        //then 
        Assert.True(actual!.IsDeleted);
    }

    [Test]
    public void RestoreClientTest_WhenCorrectIdIsPassed_ThenRestoreProfileApplied()
    {
        //given 
        var id = 2;
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        client.IsDeleted = false;

        //when 
        _sut.RemoveOrRestoreClient(client);

        var actual = _context.Clients.FirstOrDefault(c => c.Id == id);
        //then 
        Assert.False(actual.IsDeleted);
    }

    [Test]
    public void UpdatePassword_WhenCorrectDataPassed_ThenChangePassword()
    {
        //given
        string passwordForUpdate = "987654321";
        int clientId = 2;
        var client = _sut.GetClientById(clientId);
        client.Password = passwordForUpdate;

        //when
        _sut.UpdatePassword(client);

        //then
        var actualClient = _sut.GetClientById(clientId);

        Assert.AreEqual(actualClient.Password, client.Password);
    }
}
