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

    public List<Sitter> GetSittersBySearchParams(SearchParams searchEntity)
    {
        return _context.Sitters
            .Include(o => o.Orders)
            .Where(s => !s.IsDeleted &&
                s.PricesCatalog
                .Any(p => p.Service.Id == searchEntity.ServiceType &&
                p.Price >= searchEntity.PriceMinimum && p.Price <= searchEntity.PriceMaximum))
            .ToList();
    }
}
