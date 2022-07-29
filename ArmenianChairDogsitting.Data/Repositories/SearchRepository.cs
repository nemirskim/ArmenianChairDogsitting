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

    public List<Sitter> GetSittersBySearchParams(ParamsToSearchSitter searchEntity)
    {
        if (searchEntity.MinRating > 0)
            searchEntity.IsSitterHasComments = true;

        return _context.Sitters
            .Include(o => o.Orders)
            .Where(s =>
                !s.IsDeleted &&
                s.PricesCatalog.Any
                (p => (
                        p.Service.Id == searchEntity.ServiceType &&
                        (searchEntity.PriceMinimum == null || searchEntity.PriceMinimum != null && p.Price >= searchEntity.PriceMinimum) &&
                        (searchEntity.PriceMaximum == null || searchEntity.PriceMaximum != null && p.Price <= searchEntity.PriceMaximum)
                    )
                ) &&
                ((
                    searchEntity.IsSitterHasComments &&
                    s.Orders.Any(c => c.Comments.Count != 0) &&
                    (
                        searchEntity.MinRating != null &&
                        s.Orders.Any(c => c.Comments.Average(r => r.Rating) >= searchEntity.MinRating)
                    )
                ) ||
                (
                    !searchEntity.IsSitterHasComments &&
                    s.Orders.Any(c => c.Comments.Count == 0)
                )) &&
                (searchEntity.District == DistrictEnum.All || s.Districts.Any(d => d.Id == searchEntity.District))
             )
            .ToList();
    }
}//selectMany
