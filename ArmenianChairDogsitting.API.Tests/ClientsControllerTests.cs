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
        _clientsServiceMock = new Mock<IClientsService>();
        _sut = new ClientsController(_clientsServiceMock.Object, _mapper);
    }

    [Test]
    public void AddClientTest_WhenRequestPassed_ThenNewProfileCreated()
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
            .Setup(c => c.AddClient(It.IsAny<Client>()))
            .Returns(id);

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
            Id = 1
        };
        var id = 1;

        _clientsServiceMock
            .Setup(c => c.GetClientById(It.IsAny<int>()));
            //.Returns(client));

        //when
        var actual = _sut.GetClientById(id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(client.GetType(), actualResult.Value.GetType());

        _clientsServiceMock.Verify(c => c.GetClientById(id), Times.Once);
    }

    [Test]
    public void GetAllClientsTest_WhenRequestPassed_ThenShouldReturnClients()
    {
        //given
        var expected = new List<ClientAllInfoResponse>();

        //when
        var actual = _sut.GetAllClients();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expected.GetType(), actualResult.Value.GetType());
    }

    [Test]
    public void UpdateClientTest_WhenCorrectIdPassed_ThenClientProfileUpdated()
    {
        //given
        var client = new ClientUpdateRequest() { Name = "Marina" };
        var id = 1;

        //when
        var actual = _sut.UpdateClient(client, id);

        //then
        var actualResult = actual as OkResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
    }

    [Test]
    public void RemoveClientTest_WhenCorrectIdPassed_ThenSoftDeleteApplied()
    {
        //given
        var id = 1;

        //when
        var actual = _sut.RemoveClient(id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
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
    }

}
