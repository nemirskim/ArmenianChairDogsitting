

using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Repositories;

public class SitterRepository : ISitterRepository
{

    private readonly ArmenianChairDogsittingContext _context;

    public SitterRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public int Add(Sitter sitter)
    {
        _context.Sitters.Add(sitter);
        _context.SaveChanges();

        return sitter.Id;
    }

    public Sitter? GetById(int id)
    {
        var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
        var priceCatalog = _context.PriceCatalogs.Where(pr => pr.Sitter.Id == sitter.Id).ToList();
        sitter.PriceCatalog = priceCatalog;
        return sitter;
    } 

        public Sitter? GetSitterByEmail(string email) => _context.Sitters.FirstOrDefault(sitter => sitter.Email == email);

    public List<Sitter> GetSitters() => _context.Sitters
        .Where(s => !s.IsDeleted)
        .ToList();

    public void RemoveOrRestoreById(Sitter sitter)
    {
        _context.Sitters.Update(sitter);
        _context.SaveChanges();
    }

    public void Update(Sitter newSitter)
    {
        _context.Sitters.Update(newSitter);
        _context.SaveChanges();
    }

    public void UpdatePassword(Sitter SitterPasswordForUpdate)
    {
        _context.Sitters.Update(SitterPasswordForUpdate);
        _context.SaveChanges();
    }

    public void UpdatePriceCatalog(Sitter sitterWithNewPriceCatalog)
    {
        _context.Sitters.Update(sitterWithNewPriceCatalog);
        _context.SaveChanges();

/*        foreach (var price in sitterWithNewPriceCatalog.PriceCatalog)
        {
            _context.PriceCatalogs.Update(price);
            _context.SaveChanges();
            continue;
        }*/
    }
}
