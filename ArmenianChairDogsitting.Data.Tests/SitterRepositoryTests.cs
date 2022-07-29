using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests;

public class SitterRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private SitterRepository _sut;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb")
            .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();

        _sut = new SitterRepository(_context);

        _context.Sitters.Add(new Sitter()
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
        });

        _context.Sitters.Add(new Sitter()
        {
            Name = "Misha",
            LastName = "Percev",
            Phone = "89657438654",
            Email = "per@per.com",
            Password = "123456789",
            Age = 47,
            Experience = 2,
            Sex = Sex.Male,
            Description = "",
            PricesCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        });

        _context.Sitters.Add(new Sitter()
        {
            Name = "Ludmila",
            LastName = "Transformatorovna",
            Phone = "89375648354",
            Email = "trans@t.com",
            Password = "123456789",
            Age = 19,
            Experience = 3,
            Sex = Sex.Female,
            Description = "",
            PricesCatalog = new List<PriceCatalog>
            {
                new PriceCatalog
                {
                    Service = ServiceEnum.SittingForDay,
                    Price = 600,
                    Sitter = new Sitter{Name = "Ludmila"},
                },
                new PriceCatalog
                {
                    Service = ServiceEnum.DailySitting,
                    Price = 300,
                    Sitter = new Sitter{Name = "Ludmila"},
                }
            },
            Orders = new List<Order>(),
            IsDeleted = false
        }); 

        _context.Sitters.Add(new Sitter()
        {
            Name = "Svetlana",
            LastName = "Nefiltrovovna",
            Phone = "89991116116",
            Email = "svetloe@pi.com",
            Password = "123456789",
            Age = 30,
            Experience = 12,
            Sex = Sex.Female,
            Description = "",
            PricesCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = true
        });

        _context.SaveChanges();
    }

    [Test]
    public void GetById_WhenSitterIsExist_ThenReturnSitter()
    {
        //given
        var expectedSitter = new Sitter
        {
            Id = 3,
            Name = "Ludmila",
            LastName = "Transformatorovna",
            Phone = "89375648354",
            Email = "trans@t.com",
            Password = "123456789",
            Age = 19,
            Experience = 3,
            Sex = Sex.Female
        };

        //when
        var actualSitter = _sut.GetById(3);

        //then
        Assert.AreEqual(expectedSitter.Id, actualSitter.Id);
        Assert.AreEqual(expectedSitter.Name, actualSitter.Name);
        Assert.AreEqual(expectedSitter.LastName, actualSitter.LastName);
        Assert.AreEqual(expectedSitter.Email, actualSitter.Email);
    }

    [Test]
    public void GetSitters_WhenCalled_ReturnsAllSitters()
    {
        //given
        int expectedCount = 3;

        //when
        var actualSitters = _sut.GetSitters();

        //then

        Assert.NotNull(actualSitters);
        Assert.AreEqual(actualSitters.Count, expectedCount);
    }

    [Test]
    public void RemoveOrRestoreById_WhenSitterIsNotDeleted_ThenSitterDelete()
    {
        //given
        int sitterId = 2;
        var sitter = _sut.GetById(sitterId);
        sitter.IsDeleted = true;

        //when
        _sut.RemoveOrRestoreById(sitter);

        //then
        var actualSitter = _sut.GetById(sitterId);

        Assert.NotNull(actualSitter);
        Assert.True(actualSitter.IsDeleted);
    }

    [Test]
    public void RemoveOrRestoreById_WhenSitterIsDeleted_ThenSitterRestored()
    {
        //given
        int sitterId = 4;
        var sitter = _sut.GetById(sitterId);
        sitter.IsDeleted = false;

        //when
        _sut.RemoveOrRestoreById(sitter);

        //then
        var actualSitter = _sut.GetById(sitterId);

        Assert.NotNull(actualSitter);
        Assert.False(actualSitter.IsDeleted);
    }

    [Test]
    public void Update_WhenCalled_ThenUpdateProperties()
    {
        //given
        int sitterId = 1;
        var sitter = _sut.GetById(sitterId);

        sitter.Name = "Evgeniu";
        sitter.LastName = "Ponasenkov";
        sitter.Phone = "89997778866";
        //when
        _sut.Update(sitter);

        //then
        var actualSitter = _sut.GetById(sitterId);

        Assert.AreEqual(actualSitter.Name, sitter.Name);
        Assert.AreEqual(actualSitter.LastName, sitter.LastName);
        Assert.AreEqual(actualSitter.Phone, sitter.Phone);
        Assert.AreEqual(actualSitter.Age, sitter.Age);
        Assert.AreEqual(actualSitter.Experience, sitter.Experience);
        Assert.AreEqual(actualSitter.Sex, sitter.Sex);
        Assert.AreEqual(actualSitter.Description, sitter.Description);
    }

    [Test]
    public void UpdatePassword_WhenCorrectDataPassed_ThenChangePassword()
    {
        //given
        string passwordForUpdate = "987654321";
        int sitterId = 2;
        var sitter = _sut.GetById(sitterId);
        sitter.Password = passwordForUpdate;

        //when
        _sut.UpdatePassword(sitter);

        //then
        var actualSitter = _sut.GetById(sitterId);

        Assert.AreEqual(actualSitter.Password, sitter.Password);
    }

    [Test]
    public void UpdatePriceCatalog_WhenCorrectDataPassed_ThenChangePriceCatalog()
    {
        //given
        var priceCatalogForUpdate = new List<PriceCatalog>
        {
            new PriceCatalog
            {
                Service = ServiceEnum.SittingForDay,
                Price = 2000,
                Sitter = new Sitter{Name = "Ludmila"},
            },
            new PriceCatalog
            {
                Service = ServiceEnum.DailySitting,
                Price = 800,
                Sitter = new Sitter
                {
                    Name = "Ludmila",
                    LastName = "Pistoletov",
                    Phone = "89991116116",
                    Email = "pistol@pi.com",
                    Password = "123456789",
                    Age = 27,
                    Experience = 7,
                    Sex = Sex.Female,
                    Description = ""
                }
            },
            new PriceCatalog
            {
                Service = ServiceEnum.Overexpose,
                Price = 300,
                Sitter = new Sitter
                {
                    Name = "Ludmila",
                    LastName = "Pistoletov",
                    Phone = "89991116116",
                    Email = "pistol@pi.com",
                    Password = "123456789",
                    Age = 27,
                    Experience = 7,
                    Sex = Sex.Female,
                    Description = ""
                }
            }
        };

        int sitterId = 3;
        var sitter = _sut.GetById(sitterId);

        sitter.PricesCatalog = priceCatalogForUpdate;

        //when
        _sut.UpdatePriceCatalog(sitter);

        //then
        var actualSitter = _sut.GetById(sitterId);

        Assert.AreEqual(actualSitter.PricesCatalog[0].Price, priceCatalogForUpdate[0].Price);
        Assert.AreEqual(priceCatalogForUpdate[1].Price, actualSitter.PricesCatalog[1].Price);
        Assert.AreEqual(priceCatalogForUpdate[2].Price, actualSitter.PricesCatalog[2].Price);
        Assert.AreEqual(actualSitter.PricesCatalog.Count, priceCatalogForUpdate.Count);
        Assert.AreEqual(actualSitter.PricesCatalog[0].Service, priceCatalogForUpdate[0].Service);
        Assert.AreEqual(actualSitter.PricesCatalog[1].Service, priceCatalogForUpdate[1].Service);
        Assert.AreEqual(actualSitter.PricesCatalog[2].Service, priceCatalogForUpdate[2].Service);
    }
}
