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

        }
    }
}