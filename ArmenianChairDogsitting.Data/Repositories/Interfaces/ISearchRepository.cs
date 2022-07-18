using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories.Interfaces;

public interface ISearchRepository
{
    List<Sitter> GetSitters(SearchParams searchEntity);
}
