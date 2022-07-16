using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;

namespace ArmenianChairDogsitting.Business.Services;

public class SearchService : ISearchRepository
{
    ISearchRepository _searchRepository;

    public SearchService(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    public List<Sitter> GetSitters(Search searchEntity) => _searchRepository.GetSitters(searchEntity);
}
