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
}
