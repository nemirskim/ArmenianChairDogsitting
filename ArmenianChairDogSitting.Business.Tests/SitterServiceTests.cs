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
    public void AddSitter_WhenValidPassed_ThenReturnIdOfAddedSitter()
    {
        //given
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

        _sitterRepository.Setup(c => c.Add(sitter))
             .Returns(expectedId);

        //when
        var actual = _sut.Add(sitter);


        //then

        Assert.True(actual == expectedId);
        _sitterRepository.Verify(c => c.Add(sitter), Times.Once);
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
            .Setup(x => x.GetById(expectedSitter.Id))
            .Returns(expectedSitter);

        //when
        var actualSitter = _sut.GetById(1);

        //then
        Assert.True(actualSitter.Id == expectedSitter.Id);
        Assert.True(actualSitter.Name == expectedSitter.Name);
        Assert.True(actualSitter.LastName == expectedSitter.LastName);
        Assert.True(actualSitter.Email == expectedSitter.Email);
        Assert.True(actualSitter.Phone == expectedSitter.Phone);
        
        _sitterRepository.Verify(x => x.GetById(expectedSitter.Id), Times.Once);
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
    public void RemoveOrRestoreById_WhenIsDeletedEqualsFalse_ThenDeleteSitter()
    {
        ///given
        var expectedSitter = new Sitter()
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
        };

        _sitterRepository.Setup(o => o.RemoveOrRestoreById(expectedSitter.Id));

        expectedSitter.IsDeleted = true;

        _sitterRepository.Setup(o => o.GetById(expectedSitter.Id)).Returns(expectedSitter);


        //when
        _sut.RemoveOrRestoreById(expectedSitter.Id);


        //then

        var allSitters = _sut.GetSitters();
        var actualSitter = _sut.GetById(10);

        Assert.True(allSitters is null);
        Assert.True(actualSitter.IsDeleted);

        _sitterRepository.Verify(c => c.RemoveOrRestoreById(expectedSitter.Id), Times.Once);
        _sitterRepository.Verify(c => c.GetById(expectedSitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.GetSitters(), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenIsDeletedEqualsTrue_ThenRestoreSitter()
    {
        ///given
        var expectedSitter = new Sitter()
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
            IsDeleted = true
        };

        _sitterRepository.Setup(o => o.RemoveOrRestoreById(expectedSitter.Id));

        expectedSitter.IsDeleted = false;

        _sitterRepository.Setup(o => o.GetById(expectedSitter.Id)).Returns(expectedSitter);
        _sitterRepository.Setup(o => o.GetSitters()).Returns(new List<Sitter> { expectedSitter });


        //when
        _sut.RemoveOrRestoreById(expectedSitter.Id);


        //then

        var allSitters = _sut.GetSitters();
        var actualSitter = _sut.GetById(10);

        Assert.True(allSitters is not null);
        Assert.True(!actualSitter.IsDeleted);

        _sitterRepository.Verify(c => c.RemoveOrRestoreById(expectedSitter.Id), Times.Once);
        _sitterRepository.Verify(c => c.GetById(expectedSitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.GetSitters(), Times.Once);
    }

    [Test]
    public void UpdateSitter_WhenValidPassed_ThenUpdateProperties()
    {
        //given

        var sitter = new Sitter()
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
        };


        var sitterForUpdate = new Sitter()
        {
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = ""
        };

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);
        _sitterRepository.Setup(o => o.Update(sitterForUpdate, sitter.Id));


        //when
        _sut.Update(sitterForUpdate, sitter.Id);

        //then
        var actual = _sut.GetById(sitter.Id);


        Assert.True(sitter.Name == actual.Name);
        Assert.True(sitter.LastName == actual.LastName);
        Assert.True(sitter.Phone == actual.Phone);
        Assert.True(sitter.Age == actual.Age);
        Assert.True(sitter.Experience == actual.Experience);
        Assert.True(sitter.Sex == actual.Sex);
        Assert.True(sitter.Description == actual.Description);

        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.Update(sitterForUpdate, sitter.Id), Times.Once);
    }

    [Test]
    public void UpdatePassword_WhenValidPassed_ThenUpdatePassword()
    {
        //given

        var sitter = new Sitter()
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
        };


        var sitterPasswordForUpdate = new Sitter
        {
            Password = "987654321"
        };


        _sitterRepository.Setup(o => o.UpdatePassword(sitter.Id, sitterPasswordForUpdate));
        sitter.Password = sitterPasswordForUpdate.Password;
        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);


        //when
        _sut.UpdatePassword(sitter.Id, sitterPasswordForUpdate);

        //then
        var actual = _sut.GetById(sitter.Id);


        Assert.True(actual.Password == sitter.Password);

        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.UpdatePassword(sitter.Id, sitterPasswordForUpdate), Times.Once);
    }

    [Test]
    public void UpdatePriceCatalog_WhenValidPassed_ThenUpdatePriceCatalog()
    {
        //given

        var sitter = new Sitter()
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
        };


        var priceCatalogForUpdate = new List<PriceCatalog>
        {
            new PriceCatalog
            {
                Id = 2,
                Price = 500,
                Sitter = new Sitter {Id = 1 },
                Service = new Service {Id = ServiceEnum.DailySitting }
            }
        };

        _sitterRepository.Setup(o => o.UpdatePriceCatalog(sitter.Id, priceCatalogForUpdate));
        sitter.PricesCatalog = priceCatalogForUpdate;
        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when
        _sut.UpdatePriceCatalog(sitter.Id, priceCatalogForUpdate);

        //then
        var actual = _sut.GetById(sitter.Id);


        Assert.True(actual.PricesCatalog is not null);
        Assert.True(actual.PricesCatalog[0].Price == sitter.PricesCatalog[0].Price);
        Assert.True(actual.PricesCatalog[0].Id == sitter.PricesCatalog[0].Id);
        Assert.True(actual.PricesCatalog[0].Sitter.Id == sitter.PricesCatalog[0].Sitter.Id);
        Assert.True(actual.PricesCatalog[0].Service.Id == sitter.PricesCatalog[0].Service.Id);

        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.UpdatePriceCatalog(sitter.Id, priceCatalogForUpdate), Times.Once);
    }
}