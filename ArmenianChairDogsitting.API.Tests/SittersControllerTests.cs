using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;
using Moq;

namespace ArmenianChairDogsitting.API.Tests;

public class SittersControllerTests
{
    private SittersController _sut;
    private Mock<ISittersService> _sittersServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new APIMapperConfigStorage());
        });
        _mapper = mockMapper.CreateMapper();
        _sittersServiceMock = new Mock<ISittersService>();
        _sut = new SittersController(_sittersServiceMock.Object, _mapper);
    }

    [Test]
    public void AddSitter_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        var id = 23;
        var sitter = new SitterRequest()
        {
            Name = "Lena",
            LastName = "Sedunova",
            Phone = "89347630381",
            Email = "liena@mail.com",
            Password = "sirtan",
            Age = 27,
            Experience = 7,
            Description = "I love doggy",
            Sex = Sex.Female
        };

        _sittersServiceMock
            .Setup(c => c.Add(It.IsAny<Sitter>())).Returns(id);

        //when
        var actual = _sut.AddSitter(sitter);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.AreEqual(id, actualResult.Value);

        _sittersServiceMock.Verify(s => s.Add(It.Is<Sitter>(s =>
            s.Name == sitter.Name &&
            s.LastName == sitter.LastName &&
            s.Phone == sitter.Phone &&
            s.Email == sitter.Email &&
            s.Password == sitter.Password &&
            s.Age == sitter.Age &&
            s.Experience == sitter.Experience &&
            s.Sex == sitter.Sex &&
            s.Description == sitter.Description
            )), Times.Once);
    }

    [Test]
    public void GetSitterById_ValidRequestPassed_OkReceived()
    {
        //given
        var sitter = new Sitter()
        {
            Id = 1,
            Name = "Lee",
            LastName = "Takami",
            Phone = "+79265418392",
            Email = "traktor@gmail.com",
            Password = "123456789",
            Age = 27,
            Experience = 4,
            Sex = Sex.Male,
            Description = "",
            PriceCatalog = new List<PriceCatalog>()
        };

        _sittersServiceMock
            .Setup(c => c.GetById(sitter.Id)).Returns(sitter);

        //when
        var actual = _sut.GetSitterById(sitter.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var sitterMainInfoResponse = actualResult.Value as SitterMainInfoResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.Multiple(() =>
        {
            Assert.That(sitterMainInfoResponse.Id, Is.EqualTo(sitter.Id));
            Assert.That(sitterMainInfoResponse.Name, Is.EqualTo(sitter.Name));
            Assert.That(sitterMainInfoResponse.LastName, Is.EqualTo(sitter.LastName));
            Assert.That(sitterMainInfoResponse.Phone, Is.EqualTo(sitter.Phone));
            Assert.That(sitterMainInfoResponse.Email, Is.EqualTo(sitter.Email));
            Assert.That(sitterMainInfoResponse.Age, Is.EqualTo(sitter.Age));
            Assert.That(sitterMainInfoResponse.Experience, Is.EqualTo(sitter.Experience));
            Assert.That(sitterMainInfoResponse.Sex, Is.EqualTo(sitter.Sex));
            Assert.That(sitterMainInfoResponse.Description, Is.EqualTo(sitter.Description));
        });

        _sittersServiceMock.Verify(c => c.GetById(sitter.Id), Times.Once);
    }

    [Test]
    public void UpdateSitterById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var id = 1;
        var sitterToUpdate = new SitterUpdateRequest()
        {
            Name = "Lee",
            LastName = "Takami",
            Phone = "+79265418392",
            Age = 27,
            Experience = 4,
            Sex = Sex.Male,
            Description = "",
        };

        _sittersServiceMock.Setup(c => c.Update(It.IsAny<Sitter>(), id));

        //when
        var actual = _sut.UpdateSitter(sitterToUpdate);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _sittersServiceMock.Verify(s => s.Update(It.Is<Sitter>(s =>
            s.Name == sitterToUpdate.Name &&
            s.LastName == sitterToUpdate.LastName &&
            s.Phone == sitterToUpdate.Phone &&
            s.Age == sitterToUpdate.Age &&
            s.Experience == sitterToUpdate.Experience &&
            s.Sex == sitterToUpdate.Sex &&
            s.Description == sitterToUpdate.Description
        ), It.Is<int>(i => i == id)), Times.Once);
    }

    [Test]
    public void GetAllSitters_ValidRequestPassed_OkReceived()
    {
        //given
        var sitters = new List<Sitter>
        {
            new Sitter()
            {
                Id = 323,
                Name = "Kevin",
                LastName = "Durant",
                Phone = "+79651238738",
                Email = "ar@gmail.com",
                Experience = 6
            },

            new Sitter()
            {
                Id = 11,
                Name = "Mick",
                LastName = "Rock",
                Phone = "+79465412492",
                Email = "rock@mail.ru",
                Experience = 10
            }
        };

        _sittersServiceMock
            .Setup(c => c.GetSitters()).Returns(sitters);

        //when
        var actual = _sut.GetAllSitters();

        //then
        var actualResult = actual.Result as ObjectResult;
        var sitterssAllInfoResponse = actualResult.Value as List<SitterAllInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        Assert.Multiple(() =>
        {
            Assert.That(sitterssAllInfoResponse.Count, Is.EqualTo(sitters.Count));
            Assert.That(sitterssAllInfoResponse[0].Name, Is.EqualTo(sitters[0].Name));
            Assert.That(sitterssAllInfoResponse[1].Name, Is.EqualTo(sitters[1].Name));
            Assert.That(sitterssAllInfoResponse[1].LastName, Is.EqualTo(sitters[1].LastName));
            Assert.That(sitterssAllInfoResponse[0].LastName, Is.EqualTo(sitters[0].LastName));
            Assert.That(sitterssAllInfoResponse[1].Experience, Is.EqualTo(sitters[1].Experience));
            Assert.That(sitterssAllInfoResponse[0].Experience, Is.EqualTo(sitters[0].Experience));
        });

        _sittersServiceMock.Verify(c => c.GetSitters(), Times.Once);
    }

    /*[Test]
    public void RemoveSitterById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var id = 1;

        //when
        var actual = _sut.RemoveSitterById(id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _sittersServiceMock.Verify(c => c.RemoveOrRestoreById(id, true), Times.Once);
    }

    [Test]
    public void RestoreSitterById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var id = 1;

        //when
        var actual = _sut.RestoreSitterById(id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _sittersServiceMock.Verify(c => c.RemoveOrRestoreById(id, false), Times.Once);
    }*/

    [Test]
    public void UpdatePasswordSitter_ValidRequestPassed_NoContentReceived()
    {
        //given
        var id = 1;
        var sitterToUpdate = new UserUpdatePasswordRequest()
        {
            Password = "0987654321",
            OldPassword = "1234567890"
        };

        _sittersServiceMock.Setup(c => c.UpdatePassword(id, It.IsAny<User>()));

        //when
        var actual = _sut.UpdatePasswordSitter(sitterToUpdate);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _sittersServiceMock.Verify(s => s.UpdatePassword(It.Is<int>(i => i == id), It.Is<User>(s =>
            s.Password == sitterToUpdate.Password
        )), Times.Once);
    }

    [Test]
    public void UpdatePriceCatalogSitter_ValidRequestPassed_NoContentReceived()
    {
        //given
        var id = 1;
        var sitterToUpdate = new SitterUpdatePriceCatalogRequest()
        {
            PriceCatalog = new List<PriceCatalogRequest>
            {
                new PriceCatalogRequest
                {
                    Service = Service.DailySitting,
                    Price = 600
                },
                new PriceCatalogRequest
                {
                    Service = Service.Overexpose,
                    Price = 300
                }
            }
        };

        _sittersServiceMock.Setup(c => c.UpdatePriceCatalog(id, It.IsAny<Sitter>()));

        //when
        var actual = _sut.UpdatePriceCatalogSitter(sitterToUpdate);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _sittersServiceMock.Verify(s => s.UpdatePriceCatalog(It.Is<int>(i => i == id), It.Is<Sitter>(s =>
            s.PriceCatalog[0].Service == sitterToUpdate.PriceCatalog[0].Service &&
            s.PriceCatalog[1].Service == sitterToUpdate.PriceCatalog[1].Service &&
            s.PriceCatalog[0].Price == sitterToUpdate.PriceCatalog[0].Price &&
            s.PriceCatalog[1].Price == sitterToUpdate.PriceCatalog[1].Price
        )), Times.Once);
    }
}
