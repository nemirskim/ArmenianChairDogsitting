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
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfigStorage());
            });
            _mapper = mapper.CreateMapper();

            _searchRepository = new Mock<ISearchRepository>();
            _sut = new SearchService(_searchRepository.Object, _mapper);

            var districtOne = new District() { Id = DistrictEnum.Kalininsky };
            var districtTwo = new District() { Id = DistrictEnum.Tsentralny };
            var districtThree = new District() { Id = DistrictEnum.Primorsky };

            var serviceWalk = new Service() { Id = ServiceEnum.Walk };
            var serviceOverexpose = new Service() { Id = ServiceEnum.Overexpose };          

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
                            Price = 2500, Service = serviceOverexpose
                        },
                    {
                        new PriceCatalog()
                        {
                            Price = 1500, Service = serviceWalk
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
                    Districts = new List<District> { districtThree, districtTwo },
                    Orders = new List<Order>() { new OrderWalk()
                { Comments = new List<Comment>() { new Comment() { Rating = 4, Text = "blaah blah" } } } },
                    PricesCatalog = new List<PriceCatalog>()
                        {
                        new PriceCatalog()
                        {
                            Price = 3500, Service = serviceOverexpose
                        },
                        {
                        new PriceCatalog()
                        {
                            Price = 2000, Service = serviceWalk
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
                    Districts = new List<District> { districtOne, districtThree },
                    Orders = new List<Order>() { new OrderWalk()
                { Comments = new List<Comment>() { } } },
                    PricesCatalog = new List<PriceCatalog>()
                        {
                        new PriceCatalog()
                        {
                            Price = 2000, Service = serviceOverexpose
                        },
                        {
                        new PriceCatalog()
                        {
                            Price = 1500, Service = serviceWalk
                        }
                        }
                    }
                }
            };
        }

        //District - Comments - Rating
        [Test]
        public void GetSittersBySearchPaarams_RatingAndDistrictPassed_OneSittersReceived()
        {
            //given
            var searchParams = new ParamsToSearchSitter()
            {
                MinRating = 3,
                District = DistrictEnum.Kalininsky,
            };
            var expectedSittersQuantity = 1;

            _searchRepository
                .Setup(x => x.GetSittersBySearchParams(It.IsAny<ParamsToSearchSitter>()))
                .Returns(_sitterList);

            //when
            var actual = _sut.GetSittersBySearchParams(searchParams);

            //then
            Assert.AreEqual(expectedSittersQuantity, actual.Count);
            _searchRepository.Verify(x => x.GetSittersBySearchParams(searchParams), Times.Once);
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

            _searchRepository
                .Setup(x => x.GetSittersBySearchParams(It.IsAny<ParamsToSearchSitter>()))
                .Returns(_sitterList);

            //when
            var actual = _sut.GetSittersBySearchParams(searchParams);

            //then
            Assert.AreEqual(expectedSittersQuantity, actual.Count);
            _searchRepository.Verify(x => x.GetSittersBySearchParams(searchParams), Times.Once);
        }

        [Test]
        public void GetSittersBySearchParams_ComentsAndDistrictPassed_OneSittersReceived()
        {
            //given
            var searchParams = new ParamsToSearchSitter()
            {
                District = DistrictEnum.All,
                IsSitterHasComments = false,
            };

            var expectedSittersQuantity = 1;

            _searchRepository
                .Setup(x => x.GetSittersBySearchParams(It.IsAny<ParamsToSearchSitter>()))
                .Returns(_sitterList);

            //when
            var actual = _sut.GetSittersBySearchParams(searchParams);

            //then
            Assert.AreEqual(expectedSittersQuantity, actual.Count);
            _searchRepository.Verify(x => x.GetSittersBySearchParams(searchParams), Times.Once);
        }
    }
}