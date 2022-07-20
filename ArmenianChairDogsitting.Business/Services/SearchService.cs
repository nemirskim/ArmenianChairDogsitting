using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
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

    public List<SittersSearchModelResult> GetSittersBySearchParams(SearchParams searchEntity)
    {
        var sitters = _searchRepository.GetSittersBySearchParams(searchEntity);
        var sittersModel = _mapper.Map<List<SittersSearchModelResult>>(sitters);

        foreach(var sitter in sittersModel)
        {
            if(!(sitter.Districts.Any(d => d.Id == searchEntity.District) &&
                (searchEntity.IsSitterHasComments && sitter.Comments.Count != 0) &&
                sitter.Comments.Average(r => r.Rating) >= searchEntity.MinRating))
            {
                sittersModel.Remove(sitter);
            }                
        }
        return sittersModel;
    }
}
