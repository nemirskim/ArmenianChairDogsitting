using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.API.Tests;

public class ClientsControllerTests
{
    private ClientsController _sut;
    private Mock<IClientsService> _clientsServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new APIMapperConfigStorage());
        });
        _mapper = mockMapper.CreateMapper();
        _clientsServiceMock = new Mock<IClientsService>();
        _sut = new ClientsController(_clientsServiceMock.Object, _mapper);
    }

    [Test]
    public void AddClientTest_WhenRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var id = 23;
        var client = new ClientRegistrationRequest()
        {
            Name = "Lena",
            LastName = "Sedunova",
            Phone = "89347630381",
            Email = "liena@mail.com",
            Password = "sirtan"
        };

        _clientsServiceMock
            .Setup(c => c.AddClient(It.IsAny<Client>())).Returns(id);

        //when
        var actual = _sut.AddClient(client);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.AreEqual(id, actualResult.Value);

        _clientsServiceMock.Verify(c => c.AddClient(It.Is<Client>(c =>
            c.Name == client.Name &&
            c.LastName == client.LastName &&
            c.Phone == client.Phone &&
            c.Email == client.Email &&
            c.Password == client.Password
            )), Times.Once);
    }

    [Test]
    public void GetClientByIdTest_WhenCorrectIdPassed_ThenReturnExpectedClient()
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Lee",
            LastName = "Takami",
            Phone = "+79265418392",
            Email = "traktor@gmail.com"
        };

        _clientsServiceMock
            .Setup(c => c.GetClientById(client.Id)).Returns(client);

        //when
        var actual = _sut.GetClientById(client.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var clientMainInfoResponse = actualResult.Value as ClientMainInfoResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.Multiple(() =>
        {
            Assert.That(clientMainInfoResponse.Id, Is.EqualTo(client.Id));
            Assert.That(clientMainInfoResponse.Name, Is.EqualTo(client.Name));
            Assert.That(clientMainInfoResponse.LastName, Is.EqualTo(client.LastName));
            Assert.That(clientMainInfoResponse.Address, Is.EqualTo(client.Address));
            Assert.That(clientMainInfoResponse.Phone, Is.EqualTo(client.Phone));
            Assert.That(clientMainInfoResponse.Email, Is.EqualTo(client.Email));
        });

        _clientsServiceMock.Verify(c => c.GetClientById(client.Id), Times.Once);
    }

    [Test]
    public void GetAllClientsTest_WhenRequestPassed_ThenShouldReturnClients()
    {
        //given
        var clients = new List<Client>
        {
            new Client()
            {
                Id = 323,
                Name = "Kevin",
                LastName = "Durant",
                Phone = "+79651238738",
                Email = "ar@gmail.com"
            },

            new Client()
            {
                Id = 11,
                Name = "Mick",
                LastName = "Rock",
                Phone = "+79465412492",
                Email = "rock@mail.ru"
            }
        };

        _clientsServiceMock
            .Setup(c => c.GetAllClients()).Returns(clients);

        //when
        var actual = _sut.GetAllClients();

        //then
        var actualResult = actual.Result as ObjectResult;
        var clientsAllInfoResponse = actualResult.Value as List<ClientAllInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        Assert.Multiple(() =>
        {
            Assert.That(clientsAllInfoResponse.Count, Is.EqualTo(clients.Count));
            Assert.That(clientsAllInfoResponse[0].Name, Is.EqualTo(clients[0].Name));
            Assert.That(clientsAllInfoResponse[1].Name, Is.EqualTo(clients[1].Name));
            Assert.That(clientsAllInfoResponse[1].LastName, Is.EqualTo(clients[1].LastName));
            Assert.That(clientsAllInfoResponse[0].LastName, Is.EqualTo(clients[0].LastName));
            Assert.That(clientsAllInfoResponse[1].Phone, Is.EqualTo(clients[1].Phone));
            Assert.That(clientsAllInfoResponse[0].Phone, Is.EqualTo(clients[0].Phone));
            Assert.That(clientsAllInfoResponse[0].Email, Is.EqualTo(clients[0].Email));
            Assert.That(clientsAllInfoResponse[1].Email, Is.EqualTo(clients[1].Email));
        });

        _clientsServiceMock.Verify(c => c.GetAllClients(), Times.Once);
    }

    [Test]
    public void UpdateClientTest_WhenCorrectIdPassed_ThenClientProfileUpdated()
    {
        //given
        var id = 1;
        var clientToUpdate = new ClientUpdateRequest()
        {
            Name = "Lea",
            LastName = "Lea",
            Phone = "+79450186206",
            Address = "Pupkovo 18"
        };

        _clientsServiceMock.Setup(c => c.UpdateClient(It.IsAny<Client>(), id));

        //when
        var actual = _sut.UpdateClient(clientToUpdate, id);

        //then
        var actualResult = actual as OkResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        _clientsServiceMock.Verify(c => c.UpdateClient(It.Is<Client>(c =>
            c.Name == clientToUpdate.Name &&
            c.LastName == clientToUpdate.LastName &&
            c.Phone == clientToUpdate.Phone &&
            c.Address == clientToUpdate.Address
        ), It.Is<int>(i => i == id)), Times.Once);
    }

    [Test]
    public void RemoveClientTest_WhenCorrectIdPassed_ThenNoContentResultReceived()
    {
        //given
        var id = 1;

        //when
        var actual = _sut.RemoveClient(id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _clientsServiceMock.Verify(c => c.RemoveOrRestoreClient(id, true), Times.Once);
    }

    [Test]
    public void RestoreClientTest_WhenCorrectIdIsPassed_ThenRestoreClientProfile()
    {
        //given
        var id = 1;

        //when
        var actual = _sut.RestoreClient(id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _clientsServiceMock.Verify(c => c.RemoveOrRestoreClient(id, false), Times.Once);
    }

}