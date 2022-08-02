using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Moq;

namespace ArmenianChairDogsitting.API.Tests;

public class SearchControllerTests
{
    private SearchController _sut;
    private Mock<ISearchService> _searchServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new APIMapperConfigStorage());
        });
        _mapper = mockMapper.CreateMapper();

        _searchServiceMock = new Mock<ISearchService>();

        _sut = new SearchController(_searchServiceMock.Object, _mapper);
    }

    [Test]
    public void GetSittersBySearchParams_WhenParamsMatched_ReturnSitters()
    {
        //given
        var sittersFromService = GetSittersFromService();
        _searchServiceMock
            .Setup(x => x.GetSittersBySearchParams(It.IsAny<ParamsToSearchSitter>()))
            .Returns(sittersFromService);

        var searchRequest = new SearchRequest()
        {
            MinRating = 5,
            IsSitterHasComments = true,
            District = DistrictEnum.Kalininsky,
            ServiceType = ServiceEnum.Overexpose,
            PriceMinimum = 2500,
            PriceMaximum = 4000
        };

        var searchParams = new ParamsToSearchSitter()
        {
            MinRating = 5,
            IsSitterHasComments = true,
            District = DistrictEnum.Kalininsky,
            ServiceType = ServiceEnum.Overexpose,
            PriceMinimum = 2500,
            PriceMaximum = 4000
        };

        //when
        var actual = _sut.GetSittersBySearchParams(searchRequest);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualValue = actualResult.Value as List<SitterAllInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(sittersFromService.Count, actualValue.Count);
        _searchServiceMock.Verify(x => x.GetSittersBySearchParams(It.Is<ParamsToSearchSitter>(s =>
            s.MinRating == searchRequest.MinRating &&
            s.PriceMinimum == searchRequest.PriceMinimum &&
            s.PriceMaximum == searchRequest.PriceMaximum &&
            s.ServiceType == searchRequest.ServiceType &&
            s.District == searchRequest.District &&
            s.IsSitterHasComments == searchRequest.IsSitterHasComments
        )), Times.Once);
    }
        
    private List<SittersSearchModelResult> GetSittersFromService()
    {
        var districtOne = new District() { Id = DistrictEnum.Kalininsky };
        var districtTwo = new District() { Id = DistrictEnum.Tsentralny };

        var ServiceWalk = ServiceEnum.Walk;
        var ServiceOverexpose = ServiceEnum.Overexpose;

        var result = new List<SittersSearchModelResult>()
            {
                new SittersSearchModelResult
                {
                    Name = "iamname",
                    LastName = "lstname",
                    Districts = new List<District> { districtOne,  districtTwo},
                    Comments = new List<Comment>() {
                    new Comment() { Rating = 2, Text = "blaah blah" },
                    new Comment() { Rating = 3, Text = "blaah blah" },
                    new Comment() { Rating = 4, Text = "blaah blah" }},
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
                },


                new SittersSearchModelResult
                {
                    Name = "iamname",
                    LastName = "lstname",
                    Districts = new List<District> { districtOne, districtTwo },
                    Comments = new List<Comment>() { new Comment() { Rating = 4, Text = "blaah blah" } },
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
                },

                new SittersSearchModelResult
                {
                    Name = "iamname",
                    LastName = "lstname",
                    Districts = new List<District> { districtOne, districtTwo },
                    Comments = new List<Comment>() { new Comment() },
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
                }
            };
        return result;
    }
}
