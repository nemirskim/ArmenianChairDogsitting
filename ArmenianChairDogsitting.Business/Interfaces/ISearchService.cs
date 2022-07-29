using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ISearchService
{
    List<SittersSearchModelResult> GetSittersBySearchParams(ParamsToSearchSitter searchEntity);
}
