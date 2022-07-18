using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Moq;
using System.Security.Claims;

namespace ArmenianChairDogSitting.Business.Tests;

public class SitterServiceTests
{
    private Mock<ISitterRepository> _sitterRepository;
    private SitterService _sut;

    [SetUp]
    public void Setup()
    {
        _sitterRepository = new Mock<ISitterRepository>();
        _sut = new SitterService(_sitterRepository.Object);
    }

    [Test]
    public void AddSitter_WhenValidRequestPassed_ThenReturnIdOfAddedSitter()
    {
        //given
        _sitterRepository.Setup(c => c.Add(It.IsAny<Sitter>()))
             .Returns(1);
        var expectedId = 1;

        var sitter = new Sitter()
        {
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PricesCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        //when
        var actual = _sut.Add(sitter);


        //then

        Assert.True(actual == expectedId);
        _sitterRepository.Verify(c => c.Add(It.IsAny<Sitter>()), Times.Once);
    }

    [Test]
    public void GetSitterById_WhenSitterExist_ThenReturnSitter()
    {
        //given
        var expectedSitter = new Sitter()
        {
            Id = 1,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PricesCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        _sitterRepository
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(expectedSitter);

        //when
        var actualSitter = _sut.GetById(1);

        //then
        Assert.True(actualSitter.Id == expectedSitter.Id);
        Assert.True(actualSitter.Name == expectedSitter.Name);
        Assert.True(actualSitter.LastName == expectedSitter.LastName);
        Assert.True(actualSitter.Email == expectedSitter.Email);
        Assert.True(actualSitter.Phone == expectedSitter.Phone);
        _sitterRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetSitterById_WhenSitterDoesNotExist_ThenReturnNotFoundExeption()
    {
        //given
        Sitter sitterFromRepo = null;

        _sitterRepository
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(sitterFromRepo);

        //when then
        Assert.Throws<NotFoundException>(() => _sut.GetById(4));
        _sitterRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetSitters_WhenExist_ThenReturnListSitters()
    {
        //given
        List<Sitter> expectedSitters = new List<Sitter> 
        { 
            new Sitter()
            {
                Id = 1,
                Name = "Alex",
                LastName = "Pistoletov",
                Phone = "89991116116",
                Email = "pistol@pi.com",
                Password = "123456789",
                Age = 27,
                Experience = 7,
                Sex = Sex.Male,
                Description = "",
                PricesCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            },
            new Sitter()
            {
                Id = 11,
                Name = "Alex",
                LastName = "Pistoletov",
                Phone = "89991116116",
                Email = "pistol@pi.com",
                Password = "123456789",
                Age = 27,
                Experience = 7,
                Sex = Sex.Male,
                Description = "",
                PricesCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            },
            new Sitter()
            {
                Id = 10,
                Name = "Alex",
                LastName = "Pistoletov",
                Phone = "89991116116",
                Email = "pistol@pi.com",
                Password = "123456789",
                Age = 27,
                Experience = 7,
                Sex = Sex.Male,
                Description = "",
                PricesCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            }
        };

        _sitterRepository
            .Setup(x => x.GetSitters())
            .Returns(expectedSitters);

        //when
        var actual = _sut.GetSitters();

        //then
        Assert.True(actual is not null);
        Assert.True(actual.Count == 3);
        Assert.True(actual is List<Sitter>);
    }

    [Test]
    public void AddSitter_WhenValidRequestPassed_ThenReturnIdOfAddedSitter()
    {
        //given
        _sitterRepository.Setup(c => c.Add(It.IsAny<Sitter>()))
             .Returns(1);
        var expectedId = 1;

        var sitter = new Sitter()
        {
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PricesCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        //when
        var actual = _sut.Add(sitter);


        //then

        Assert.True(actual == expectedId);
        _sitterRepository.Verify(c => c.Add(It.IsAny<Sitter>()), Times.Once);
    }
}