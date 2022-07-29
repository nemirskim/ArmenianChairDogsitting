using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using AutoMapper;

namespace ArmenianChairDogsitting.Business.Services;

public class SearchService : ISearchService
{
    ISearchRepository _searchRepository;
    IMapper _mapper;

    public SearchService(ISearchRepository searchRepository, IMapper mapper)
    {
        _searchRepository = searchRepository;
        _mapper = mapper;
    }

    public List<SittersSearchModelResult> GetSittersBySearchParams(ParamsToSearchSitter searchEntity)
    {
        var sitters = _searchRepository.GetSittersBySearchParams(searchEntity);
        var sittersModel = _mapper.Map<List<SittersSearchModelResult>>(sitters);

        return sittersModel;
    }
}