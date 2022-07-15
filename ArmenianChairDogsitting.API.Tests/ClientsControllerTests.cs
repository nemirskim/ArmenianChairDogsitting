using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Tests;

public class ClientsControllerTests
{
    private ClientsController _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new ClientsController();
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

        //when
        var actual = _sut.AddClient(client);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.AreEqual(id, actualResult.Value);
    }

    [Test]
    public void GetClientByIdTest_WhenCorrectIdPassed_ThenReturnExpectedClient()
    {
        //given
        var client = new ClientAllInfoResponse();
        var id = 1;

        //when
        var actual = _sut.GetClientById(id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(client.GetType(), actualResult.Value.GetType());
    }

    [Test]
    public void GetAllClientsTest_WhenRequestPassed_ThenShouldReturnClients()
    {
        //given
        var expected = new ClientAllInfoResponse();

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
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
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
