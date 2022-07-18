using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Repositories;

public class SearchRepository : ISearchRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public SearchRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public List<Sitter> GetSitters(SearchParams searchEntity)
    {
        return (List<Sitter>)_context.Sitters
            .Include(c => c.Orders)
            .Where(s =>
                s.PricesCatalog
                .Exists(p => p.Price >= searchEntity.PriceMinimum && p.Price <= searchEntity.PriceMaximum))
            .ToList();
    }
}
