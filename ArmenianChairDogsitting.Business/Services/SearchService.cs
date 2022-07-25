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
        var result = new List<SittersSearchModelResult>();
        RelocateComments(sitters, sittersModel);

        if (searchEntity.MinRating > 0)
            searchEntity.IsSitterHasComments = true;

        foreach (var sitter in sittersModel)
        {
            if(sitter.Districts.Any(d => d.Id == searchEntity.District ||
                searchEntity.District == DistrictEnum.All) &&
                ((searchEntity.IsSitterHasComments is true && sitter.Comments.Count != 0 &&
                sitter.Comments.Average(r => r.Rating) >= searchEntity.MinRating)) ||
                (searchEntity.IsSitterHasComments is false && sitter.Comments.Count == 0))
            {
                result.Add(sitter);
            }                
        }
        return result;
    }

    private void RelocateComments(List<Sitter> sitters, List<SittersSearchModelResult> sittersModel)
    {
        for(int i = 0; i < sitters.Count; i++)
        {
            foreach(var item in sitters[i].Orders)
            {
                sittersModel[i].Comments.AddRange(item.Comments);
            }
        }
    }
}
