using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class ClientsServiceTests
{
    private ClientsService _sut;
    private Mock<IClientsRepository> _clientsRepositoryMock;

    public void SetUp()
    {
        _sut = new ClientsService(_clientsRepositoryMock.Object);
        _clientsRepositoryMock = new Mock<IClientsRepository>();
    }

    [Test]
    public void AddClientTest_WhenRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var client = new Client()
        {
            Name = "Lee",
            LastName = "Takami",
            Phone = "+79265418392",
            Email = "traktor@gmail.com",
            Password = "ns32ltn"
        };
        var id = 1;

        _clientsRepositoryMock.Setup(c => c.AddClient(It.IsAny<Client>())).Returns(1);

        //when
        var actual = _sut.AddClient(client);

        //then
        Assert.AreEqual(id, actual);

        _clientsRepositoryMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);//верифаи
    }

    [Test]
    public void GetClientById_WhenRequestPassed_ClientReceived()
    {
        //given
        var client = new Client()
        {
            Id = 57,
            Name = "Ite",
            LastName = "Nat",
            Phone = "+79061911882",
            Email = "korovka@gmail.com",
        };

        _clientsRepositoryMock.Setup(c => c.GetClientById(client.Id)).Returns(client);

        //when
        var actual = _sut.GetClientById(client.Id);

        //then
        Assert.AreEqual(client, actual);

        _clientsRepositoryMock.Verify(c => c.GetClientById(client.Id), Times.Once);
    }

    [Test]
    public void GetAllClientsTest_WhenRequestPassed_ClientsReceived()
    {
        //given
        var clients = new List<Client>()
        {
            new Client()
            {
                Name = "Lee",
                LastName = "Takami",
                Phone = "+79265418392",
                Email = "traktor@gmail.com",
                Password = "ns32ltn"
            },

            new Client()
            {
                Name = "Imsi",
                LastName = "Iasn",
                Phone = "+79292018314",
                Email = "trakor@gmail.com",
                Password = "ns757932ltn"
            },

            new Client()
            {
                Name = "Leeat",
                LastName = "Tami",
                Phone = "+790283418392",
                Email = "traktor@mail.com",
                Password = "nslt.an"
            }
        };

        _clientsRepositoryMock.Setup(c => c.GetAllClients()).Returns(clients);

        //when
        var actual = _sut.GetAllClients();

        //then
        Assert.That(actual, Is.EquivalentTo(clients));
        Assert.AreEqual(clients.Count, actual.Count);

        _clientsRepositoryMock.Verify(c => c.GetAllClients(), Times.Once);
    }

    [Test]
    public void UpdateClientTest_WhenRequestPassed_UpdateClientReceived()
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Leeat",
            LastName = "Tami",
            Phone = "+790283418392",
            Email = "traktor@mail.com"
        };

        var clientToUpdate = new Client()
        {
            Name = "Leeati",
            LastName = "Tamil",
            Phone = "+790283412392",
            Email = "traktor@mail.com"
        };

        _clientsRepositoryMock.Setup(c => c.GetClientById(client.Id)).Returns(client);
        _clientsRepositoryMock.Setup(c => c.UpdateClient(clientToUpdate, client.Id)); 

        //when
        _sut.UpdateClient(client, client.Id);

        //then
        var actual = _sut.GetClientById(client.Id);

        Assert.AreEqual(client.Id, clientToUpdate.Id);
        Assert.AreNotSame(client, clientToUpdate);

        _clientsRepositoryMock.Verify(c => c.GetClientById(client.Id), Times.Exactly(2));
        _clientsRepositoryMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>()));
    }

    [Test]
    public void RemoveClientTest_WhenRequestPassed_ThenRemoveClientReceived()
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Leeat",
            LastName = "Tami",
            Phone = "+790283418392",
            Email = "traktor@mail.com",
            IsDeleted = false
        };

        _clientsRepositoryMock.Setup(c => c.RemoveOrRestoreClient(client.Id, true));

        //when
        _sut.RemoveOrRestoreClient(client.Id, true);

        //then
        Assert.IsTrue(client.IsDeleted);

        _clientsRepositoryMock.Verify(c => c.RemoveOrRestoreClient(It.IsAny<int>(), true), Times.Once);
    }

    [Test]
    public void RestoreClientTest_WhenRequestPassed_ThenRestoreClientReceived()
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Leeat",
            LastName = "Tami",
            Phone = "+790283418392",
            Email = "traktor@mail.com",
            IsDeleted = true
        };

        _clientsRepositoryMock.Setup(c => c.RemoveOrRestoreClient(client.Id, false));

        //when
        _sut.RemoveOrRestoreClient(client.Id, false);

        //then
        Assert.IsTrue(client.IsDeleted);

        _clientsRepositoryMock.Verify(c => c.RemoveOrRestoreClient(It.IsAny<int>(), false), Times.Once);
    }
}
