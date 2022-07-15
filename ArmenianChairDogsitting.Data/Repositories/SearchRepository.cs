using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;

namespace ArmenianChairDogsitting.Data.Repositories;

public class SearchRepository : ISearchRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public SearchRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public List<Sitter> GetSiiters(Search searchEntity)
    {
        _context.Sitters.ToList().Where(s => s.PricesCatalog.)
    }
}
