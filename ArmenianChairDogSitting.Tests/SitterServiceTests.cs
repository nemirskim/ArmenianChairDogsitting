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
    private Mock<ISittersRepository> _sitterRepository;
    private SittersService _sut;

    [SetUp]
    public void Setup()
    {
        _sitterRepository = new Mock<ISittersRepository>();
        _sut = new SittersService(_sitterRepository.Object);
    }

    [Test]
    public void AddSitter_WhenValidationPassed_ThenReturnIdOfAddedSitter()
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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        _sitterRepository.Setup(c => c.Add(sitter))
             .Returns(expectedId);

        //when
        var actual = _sut.Add(sitter);


        //then

        Assert.AreEqual(actual, expectedId);
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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        _sitterRepository
            .Setup(x => x.GetById(expectedSitter.Id))
            .Returns(expectedSitter);

        //when
        var actualSitter = _sut.GetById(1);

        //then
        Assert.AreEqual(actualSitter.Id, expectedSitter.Id);
        Assert.AreEqual(actualSitter.Name, expectedSitter.Name);
        Assert.AreEqual(actualSitter.LastName, expectedSitter.LastName);
        Assert.AreEqual(actualSitter.Email, expectedSitter.Email);
        Assert.AreEqual(actualSitter.Phone, expectedSitter.Phone);
        
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
                PriceCatalog = new List<PriceCatalog>(),
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
                PriceCatalog = new List<PriceCatalog>(),
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
                PriceCatalog = new List<PriceCatalog>(),
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
        Assert.AreEqual(actual.Count, expectedSitters.Count);
        Assert.True(actual is List<Sitter>);
    }

    [Test]
    public void RemoveOrRestoreById_WhenSitterIsNotDeleted_ThenDeleteSitter()
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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        _sitterRepository.Setup(o => o.GetById(expectedSitter.Id)).Returns(expectedSitter);

        //when
        _sut.RemoveOrRestoreById(expectedSitter.Id, true);


        //then

        var allSitters = _sut.GetSitters();
        var actualSitter = _sut.GetById(10);

        Assert.True(allSitters is null);
        Assert.True(actualSitter.IsDeleted);

        _sitterRepository.Verify(c => c.RemoveOrRestoreById(expectedSitter), Times.Once);
        _sitterRepository.Verify(c => c.GetById(expectedSitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.GetSitters(), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenSitterIsDeleted_ThenRestoreSitter()
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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = true
        };

        _sitterRepository.Setup(o => o.GetById(expectedSitter.Id)).Returns(expectedSitter);
        _sitterRepository.Setup(o => o.GetSitters()).Returns(new List<Sitter> { expectedSitter });


        //when
        _sut.RemoveOrRestoreById(expectedSitter.Id, false);


        //then

        var allSitters = _sut.GetSitters();
        var actualSitter = _sut.GetById(10);

        Assert.True(allSitters is not null);
        Assert.True(!actualSitter.IsDeleted);

        _sitterRepository.Verify(c => c.RemoveOrRestoreById(expectedSitter), Times.Once);
        _sitterRepository.Verify(c => c.GetById(expectedSitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.GetSitters(), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenSitterIsNotExist_ThenNotFoundExeption()
    {
        //given
        int sitterId = 1;

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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //then, when
        Assert.Throws<NotFoundException>(() => _sut.RemoveOrRestoreById(sitterId, true));
    }

    [Test]
    public void UpdateSitter_WhenValidationPassed_ThenUpdateProperties()
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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };


        var sitterForUpdate = new Sitter()
        {
            Id = 11,
            Name = "Alex",
            LastName = "Abramov",
            Phone = "89991116116",
            Email = "nepistol@pi.com",
            Password = "qwertyuio",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PriceCatalog = new List<PriceCatalog>
            {
                new PriceCatalog()
                {
                    Price = 500,
                    Service = ServiceEnum.Overexpose,
                    Sitter = new Sitter(),
                    Id = 10,
                }
            },
            Orders = new List<Order>(),
            IsDeleted = true
        };

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when
        _sut.Update(sitterForUpdate, sitter.Id);

        //then
        var actual = _sut.GetById(sitter.Id);

        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.Update(It.Is<Sitter>(s =>
        s.IsDeleted == sitter.IsDeleted &&
        s.Name == sitterForUpdate.Name &&
        s.LastName == sitterForUpdate.LastName &&
        s.Phone == sitterForUpdate.Phone &&
        s.Age == sitterForUpdate.Age &&
        s.Experience == sitterForUpdate.Experience &&
        s.Sex == sitterForUpdate.Sex &&
        s.Description == sitterForUpdate.Description &&
        s.Id == sitter.Id &&
        s.Password == sitter.Password &&
        s.Email == sitter.Email &&
        s.PriceCatalog == sitter.PriceCatalog &&
        s.Orders == sitter.Orders
        )), Times.Once);
    }

    [Test]
    public void UpdateSitter_WhenSitterIsNotExist_ThenNotFoundExeption()
    {
        //given
        int sitterId = 1;

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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };


        var sitterForUpdate = new Sitter()
        {
            Name = "Alex",
            LastName = "Abramov",
            Phone = "89991116116",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = ""
        };

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when

        //then
        Assert.Throws<NotFoundException>(() => _sut.Update(sitterForUpdate, sitterId));
    }

    [Test]
    public void UpdatePassword_WhenValidationPassed_ThenUpdatePassword()
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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };


        string sitterPasswordForUpdate = "987654321";

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when
        _sut.UpdatePassword(sitter.Id, sitterPasswordForUpdate);

        //then
        var actual = _sut.GetById(sitter.Id);


        Assert.AreEqual(actual.Password, sitter.Password);

        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.UpdatePassword(sitter), Times.Once);
    }

    [Test]
    public void UpdatePassword_WhenSitterIsNotExist_ThenNotFoundExeption()
    {
        //given
        int sitterId = 1;

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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };


        var sitterPasswordForUpdate = "987654321";

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when

        //then
        Assert.Throws<NotFoundException>(() => _sut.UpdatePassword(sitterId, sitterPasswordForUpdate));
    }

    [Test]
    public void UpdatePriceCatalog_WhenValidationPassed_ThenUpdatePriceCatalog()
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
            PriceCatalog = new List<PriceCatalog>{
                new PriceCatalog
                {
                    Id = 2,
                    Price = 500,
                    Sitter = new Sitter {Id = 1 },
                    Service = ServiceEnum.DailySitting,
                }
            },
            Orders = new List<Order>(),
            IsDeleted = false
        };


        var sitterForUpdate = new Sitter
        {
            PriceCatalog = new List<PriceCatalog>
            {
                new PriceCatalog
                {
                    Id = 1,
                    Price = 800,
                    Sitter = new Sitter {Id = 1 },
                    Service = ServiceEnum.Overexpose
                },
                new PriceCatalog
                {
                    Id = 2,
                    Price = 600,
                    Sitter = new Sitter {Id = 1 },
                    Service = ServiceEnum.DailySitting
                }
            }
        };

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when
        _sut.UpdatePriceCatalog(sitter.Id, sitterForUpdate);

        //then
        var actual = _sut.GetById(sitter.Id);


        Assert.True(actual.PriceCatalog is not null);
        Assert.AreEqual(actual.PriceCatalog[0].Price, sitter.PriceCatalog[0].Price);
        Assert.AreEqual(actual.PriceCatalog[1].Price, sitter.PriceCatalog[1].Price);
        Assert.AreEqual(actual.PriceCatalog[0].Id, sitter.PriceCatalog[0].Id);
        Assert.AreEqual(actual.PriceCatalog[1].Id, sitter.PriceCatalog[1].Id);
        Assert.AreEqual(actual.PriceCatalog[0].Sitter.Id, sitter.PriceCatalog[0].Sitter.Id);
        Assert.AreEqual(actual.PriceCatalog[1].Sitter.Id, sitter.PriceCatalog[1].Sitter.Id);
        Assert.AreEqual(actual.PriceCatalog[0].Service, sitter.PriceCatalog[0].Service);
        Assert.AreEqual(actual.PriceCatalog[1].Service, sitter.PriceCatalog[1].Service);

        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Exactly(2));
        _sitterRepository.Verify(c => c.UpdatePriceCatalog(sitter), Times.Once);
    }

    [Test]
    public void UpdatePriceCatalog_WhenSitterIsNotExist_ThenNotFoundExeption()
    {
        //given
        int sitterId = 5;

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
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };


        var sitterForUpdate = new Sitter
        {
            PriceCatalog = new List<PriceCatalog>
            {
                new PriceCatalog
                {
                    Id = 1,
                    Price = 800,
                    Sitter = new Sitter {Id = 1 },
                    Service = ServiceEnum.Overexpose
                },
                new PriceCatalog
                {
                    Id = 2,
                    Price = 600,
                    Sitter = new Sitter {Id = 1 },
                    Service = ServiceEnum.DailySitting
                }
            }
        };

        _sitterRepository.Setup(o => o.GetById(sitter.Id)).Returns(sitter);

        //when

        //then

        Assert.Throws<NotFoundException>(() => _sut.UpdatePriceCatalog(sitterId, sitterForUpdate));
    }
}