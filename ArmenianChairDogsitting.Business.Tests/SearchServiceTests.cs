using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using AutoMapper;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests
{
    public class SearchServiceTests
    {
        private Mock<ISearchRepository> _searchRepository;
        private SearchService _sut;
        private IMapper _mapper;
        private List<Sitter> _sitterList;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfigStorage());
            });
            _mapper = mockMapper.CreateMapper();

            _searchRepository = new Mock<ISearchRepository>();
            _sut = new SearchService(_searchRepository.Object, _mapper);

            var districtOne = new District() { Id = DistrictEnum.Kalininsky };
            var districtTwo = new District() { Id = DistrictEnum.Tsentralny };
            var districtThree = new District() { Id = DistrictEnum.Primorsky };

            var ServiceWalk = new Service() { Id = ServiceEnum.Walk };
            var ServiceOverexpose = new Service() { Id = ServiceEnum.Overexpose };          

            _sitterList = new List<Sitter>() 
            {
                new Sitter
                {
                    Name = "iamname",
                    Description = "qweqwe",
                    Email = "qwe qwe",
                    LastName = "lstname",
                    Password = "wwwwwww",
                    Phone = "89567234581",
                    Districts = new List<District> { districtOne,  districtTwo},
                    Orders = new List<Order>() { new OrderWalk()
                    { Comments = new List<Comment>() { 
                    new Comment() { Rating = 2, Text = "blaah blah" },
                    new Comment() { Rating = 3, Text = "blaah blah" },
                    new Comment() { Rating = 4, Text = "blaah blah" }} 
                    } },
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


                new Sitter
                {
                    Name = "iamname",
                    Description = "qweqwe",
                    Email = "qwe qwe",
                    LastName = "lstname",
                    Password = "wwwwwww",
                    Phone = "89567234581",
                    Districts = new List<District> { districtOne, districtTwo },
                    Orders = new List<Order>() { new OrderWalk()
                { Comments = new List<Comment>() { new Comment() { Rating = 4, Text = "blaah blah" } } } },
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

                new Sitter
                {
                    Name = "iamname",
                    Description = "qweqwe",
                    Email = "qwe qwe",
                    LastName = "lstname",
                    Password = "wwwwwww",
                    Phone = "89567234581",
                    Districts = new List<District> { districtOne, districtTwo },
                    Orders = new List<Order>() { new OrderWalk()
                { Comments = new List<Comment>() { new Comment() } } },
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
        }

        [Test]
        public void GetSittersBySearchPaarams_WhenParamsMatched_ReturnSitters()
        {
            //given
            var searchParams = new SearchParams()
            {
                MinRating = 3,
                IsSitterHasComments = true,
                District = DistrictEnum.Kalininsky,
                ServiceType = ServiceEnum.Overexpose,
                PriceMinimum = 2500,
                PriceMaximum = 4000
            };

            var expectedSittersQuantity = 2;

            _searchRepository
                .Setup(x => x.GetSittersBySearchParams(It.IsAny<SearchParams>()))
                .Returns(_sitterList);

            //when
            var actual = _sut.GetSittersBySearchParams(searchParams);

            //then
            Assert.AreEqual(expectedSittersQuantity, actual.Count);
            _searchRepository.Verify(x => x.GetSittersBySearchParams(searchParams), Times.Once);
        }

        [Test]
        public void GetSittersBySearchPaaramsWhenParamsNotMatched_ReturnEmpty()
        {
            //given
            var searchParams = new SearchParams()
            {
                MinRating = 5,
                IsSitterHasComments = true,
                District = DistrictEnum.Kalininsky,
                ServiceType = ServiceEnum.Overexpose,
                PriceMinimum = 2500,
                PriceMaximum = 4000
            };

            var expectedSittersQuantity = 0;

            _searchRepository
                .Setup(x => x.GetSittersBySearchParams(It.IsAny<SearchParams>()))
                .Returns(_sitterList);

            //when
            var actual = _sut.GetSittersBySearchParams(searchParams);

            //then
            Assert.AreEqual(expectedSittersQuantity, actual.Count);
            _searchRepository.Verify(x => x.GetSittersBySearchParams(searchParams), Times.Once);
        }
    }
}