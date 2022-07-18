using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ISearchService
{
    List<Sitter> GetSitters(SearchParams searchEntity);
}
