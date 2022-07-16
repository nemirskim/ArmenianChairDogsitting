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

    public List<Sitter> GetSitters(Search searchEntity)
    {
        var suitableSitters =  (List<Sitter>)_context.Sitters
            .ToList()
            .Where(s =>
            {
                var isPriceSuitable = s.PricesCatalog
                .Exists(p => p.Price > searchEntity.PriceMinimum && p.Price < searchEntity.PriceMaximum);
                var isDistrictSuitable = s.Districts.Contains(searchEntity.District);
                var isCommentsQuantitySuitable = s.CommentsQuantity == searchEntity.CommentsQuantity;
                var isRatingSuitable = s.Rating >= searchEntity.Rating;

                if (isPriceSuitable &&
                isDistrictSuitable &&
                isCommentsQuantitySuitable &&
                isRatingSuitable &&
                s.IsDeleted is false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        return suitableSitters;
    }
}
