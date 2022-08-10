using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Repositories;

public class PromocodeRepository : IPromocodeRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public PromocodeRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public Promocode? GetPromocode(string promocode) =>
        _context.Promocodes
            .Where(p => p.Promo == promocode)
            .FirstOrDefault();            
}
