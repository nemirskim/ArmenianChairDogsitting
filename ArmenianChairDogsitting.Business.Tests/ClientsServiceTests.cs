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

        _clientsRepositoryMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
    }

    [Test]
    public void GetClientById_WhenRequestPassed_ClientReceived()
    {

    }

    [Test]
    public void GetClientsTest_WhenRequestPassed_ClientsReceived()
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
        Assert.AreEqual(clients.Count, actual.Count);

        _clientsRepositoryMock.Verify(c => c.GetAllClients(), Times.Once);
    }
}
