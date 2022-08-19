using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests;

public class SearchRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private SearchRepository _sut;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
        .UseInMemoryDatabase(databaseName: $"TestDb")
        .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();
        _sut = new SearchRepository(_context);

        var districtOne = new Entities.District() { Id = Enums.District.Kalininsky };
        var districtTwo = new Entities.District() { Id = Enums.District.Tsentralny };
        var districtThree = new Entities.District() { Id = Enums.District.Primorsky };

        var ServiceWalk = Service.Walk;
        var ServiceOverexpose = Service.Overexpose;

        _context.Sitters.Add(new Sitter
        {
            Name = "iamname",
            Description = "qweqwe",
            Email = "qwe qwe",
            LastName = "lstname",
            Password = "wwwwwww",
            Phone = "89567234581",
            IsDeleted = true
        });

        _context.SaveChanges();

        _context.Sitters.Add(new Sitter
        {
            Name = "iamname",
            Description = "qweqwe",
            Email = "qwe qwe",
            LastName = "lstname",
            Password = "wwwwwww",
            Phone = "89567234581",
            Districts = new List<Entities.District> { districtOne,  districtTwo},
            Orders = new List<Order>() { new Order() 
            { Comments = new List<Comment>() } },
            PriceCatalog = new List<PriceCatalog>()
            { 
                new PriceCatalog() 
                { 
                    Price = 2500, Service = Service.Overexpose
                },
            { 
                new PriceCatalog() 
                {
                    Price = 1500, Service = Service.Walk
                }
                }
            }
        });

        _context.SaveChanges();

        _context.Sitters.Add(new Sitter
        {
            Name = "iamname",
            Description = "qweqwe",
            Email = "qwe qwe",
            LastName = "lstname",
            Password = "wwwwwww",
            Phone = "89567234581",
            Districts = new List<Entities.District> { districtOne, districtTwo },
            Orders = new List<Order>() { new Order()
            { Comments = new List<Comment>() { new Comment() { Rating = 5, Text = "blaah blah" },
            new Comment() { Rating = 3, Text = "bddd rrrr" }} } },
            PriceCatalog = new List<PriceCatalog>()
            {
                new PriceCatalog()
                {
                    Price = 3500, Service = Service.Overexpose
                },
            {
                new PriceCatalog()
                {
                    Price = 2000, Service = Service.Walk
                }
                }
            }
        });

        _context.SaveChanges();

        _context.Sitters.Add(new Sitter
        {
            Name = "iamname",
            Description = "qweqwe",
            Email = "qwe qwe",
            LastName = "lstname",
            Password = "wwwwwww",
            Phone = "89567234581",
            Districts = new List<Entities.District> { districtThree, districtTwo },
            Orders = new List<Order>() { new Order()
            { Comments = new List<Comment>() { new Comment() { Rating = 5, Text = "blaah blah"} } } },
            PriceCatalog = new List<PriceCatalog>()
            {
                new PriceCatalog()
                {
                    Price = 1500, Service = Service.Overexpose
                },
            {
                new PriceCatalog()
                {
                    Price = 1000, Service = Service.Walk
                }
                }
            }
        });

        _context.SaveChanges();
    }

    [Test]
    public void GetSittersBySeaarchParams_WhenParamsMatched_ReturnSitters()
    {
        //given
        var searchParam = new ParamsToSearchSitter()
        {
            District = Enums.District.Kalininsky,
            ServiceType = Service.Overexpose,
            PriceMinimum = 2000,
            PriceMaximum = 3600
        };
        var expectedSittersQuantity = 1;

        //when
        var actualSitters = _sut.GetSittersBySearchParams(searchParam);

        //then
        Assert.AreEqual(expectedSittersQuantity, actualSitters.Count);
    }

    [Test]
    public void GetSittersBySeaarchParams_WhenParamsNotMatched_ReturnEmpty()
    {
        //given
        var searchParam = new ParamsToSearchSitter()
        {
            District = Enums.District.Kalininsky,
            ServiceType = Service.Overexpose,
            PriceMinimum = 7000,
            PriceMaximum = 7000
        };
        var expectedSittersQuantity = 0;

        //when
        var actualSitters = _sut.GetSittersBySearchParams(searchParam);

        //then
        Assert.AreEqual(expectedSittersQuantity, actualSitters.Count);
    }

    [Test]
    public void GetSittersBySearchPaarams_RatingAndDistrictPassed_OneSittersReceived()
    {
        //given
        var searchParams = new ParamsToSearchSitter()
        {
            MinRating = 4,
            District = Enums.District.Kalininsky,
            ServiceType = Service.Overexpose
        };
        var expectedSittersQuantity = 1;

        //when
        var actual = _sut.GetSittersBySearchParams(searchParams);

        //then
        Assert.AreEqual(expectedSittersQuantity, actual.Count);
    }

    [Test]
    public void GetSittersBySearchParams_CommentsAndRatingPassed_ZeroSittersReceived()
    {
        //given
        var searchParams = new ParamsToSearchSitter()
        {
            MinRating = 3,
            IsSitterHasComments = false,
        };

        var expectedSittersQuantity = 0;


        //when
        var actual = _sut.GetSittersBySearchParams(searchParams);

        //then
        Assert.AreEqual(expectedSittersQuantity, actual.Count);
    }

    [Test]
    public void GetSittersBySearchParams_ComentsAndDistrictPassed_OneSittersReceived()
    {
        //given
        var searchParams = new ParamsToSearchSitter()
        {
            District = Enums.District.All,
            IsSitterHasComments = false,
            ServiceType = Service.Overexpose
        };

        var expectedSittersQuantity = 1;

        //when
        var actual = _sut.GetSittersBySearchParams(searchParams);

        //then
        Assert.AreEqual(expectedSittersQuantity, actual.Count);
    }
}