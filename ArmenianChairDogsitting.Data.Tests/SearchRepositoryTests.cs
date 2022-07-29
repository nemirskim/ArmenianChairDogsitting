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

        var districtOne = new District() { Id = DistrictEnum.Kalininsky };
        var districtTwo = new District() { Id = DistrictEnum.Tsentralny };
        var districtThree = new District() { Id = DistrictEnum.Primorsky };

        var ServiceWalk = new Service() { Id = ServiceEnum.Walk };
        var ServiceOverexpose = new Service() { Id = ServiceEnum.Overexpose };

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
            Districts = new List<District> { districtOne,  districtTwo},
            Orders = new List<Order>() { new OrderWalk() 
            { Comments = new List<Comment>() } },
            PricesCatalog = new List<PriceCatalog>()
            { 
                new PriceCatalog() 
                { 
                    Price = 2500, Service = ServiceOverexpose
                },
            { 
                new PriceCatalog() 
                {
                    Price = 1500, Service = ServiceWalk
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
            Districts = new List<District> { districtOne, districtTwo },
            Orders = new List<Order>() { new OrderWalk()
            { Comments = new List<Comment>() { new Comment() { Rating = 5, Text = "blaah blah" },
            new Comment() { Rating = 3, Text = "bddd rrrr" }} } },
            PricesCatalog = new List<PriceCatalog>()
            {
                new PriceCatalog()
                {
                    Price = 3500, Service = ServiceOverexpose
                },
            {
                new PriceCatalog()
                {
                    Price = 2000, Service = ServiceWalk
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
            Districts = new List<District> { districtThree, districtTwo },
            Orders = new List<Order>() { new OrderWalk()
            { Comments = new List<Comment>() { new Comment() { Rating = 5, Text = "blaah blah"} } } },
            PricesCatalog = new List<PriceCatalog>()
            {
                new PriceCatalog()
                {
                    Price = 1500, Service = ServiceOverexpose
                },
            {
                new PriceCatalog()
                {
                    Price = 1000, Service = ServiceWalk
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
            District =  DistrictEnum.Kalininsky,
            ServiceType = ServiceEnum.Overexpose,
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
            District = DistrictEnum.Kalininsky,
            ServiceType = ServiceEnum.Overexpose,
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
            District = DistrictEnum.Kalininsky,
            ServiceType = ServiceEnum.Overexpose
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
            District = DistrictEnum.All,
            IsSitterHasComments = false,
            ServiceType = ServiceEnum.Overexpose
        };

        var expectedSittersQuantity = 1;

        //when
        var actual = _sut.GetSittersBySearchParams(searchParams);

        //then
        Assert.AreEqual(expectedSittersQuantity, actual.Count);
    }
}