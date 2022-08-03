using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ArmenianChairDogsitting.API.Tests;

public class AdminControllerTests
{
    private AdminsController _sut;
    private Mock<IAdminsService> _adminServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new APIMapperConfigStorage());
        });
        _mapper = mockMapper.CreateMapper();
        _adminServiceMock = new Mock<IAdminsService>();
        _sut = new AdminsController(_adminServiceMock.Object, _mapper);
    }


    [Test]
    public void AddAdmin_ValidRequestPassed_ThenCreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var admin = new AdminRequest()
        {
            Password = "123456789",
            Email = "vita@gmail.com"
        };

        var expectedAdmin = new Admin
        {
            Password = admin.Password,
            Email = admin.Email,
            IsDeleted = false
        };

        _adminServiceMock
            .Setup(x => x.AddAdmin(It.IsAny<Admin>()))
            .Returns(expectedId);

        //when
        var actual = _sut.AddAdmin(admin);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);

        _adminServiceMock.Verify(x => x.AddAdmin(It.Is<Admin>(c =>
            c.Password == expectedAdmin.Password &&
            c.Email == expectedAdmin.Email &&
            !c.IsDeleted
        )), Times.Once);
    }

    [Test]
    public void DeleteAdminById_WhenAdminIsNotDeleted_NoContentReceived()
    {
        //given
        var expectedAdmin = new Admin()
        {
            Id = 1,
            Password = "12345678",
            Email = "AdamSmith@gmail.com",
            IsDeleted = false
        };

        _adminServiceMock.Setup(o => o.GetAdminById(expectedAdmin.Id)).Returns(expectedAdmin);

        //when
        var actual = _sut.RemoveAdminById(expectedAdmin.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.That(actualResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        _adminServiceMock.Verify(c => c.RemoveOrRestoreById(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
    }

    [Test]
    public void RestoreAdminById_WhenAdminIsDeleted_NoContentReceived()
    {
        //given
        var expectedAdmin = new Admin()
        {
            Id = 1,
            Password = "12345678",
            Email = "AdamSmith@gmail.com",
            IsDeleted = true
        };

        _adminServiceMock.Setup(o => o.GetAdminById(expectedAdmin.Id)).Returns(expectedAdmin);

        //when
        var actual = _sut.RestoreAdminById(expectedAdmin.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.That(actualResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        _adminServiceMock.Verify(c => c.RemoveOrRestoreById(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
    }
}
