using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Repositories
{
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

        public Sitter? GetById(int id) => _context.Sitters.FirstOrDefault(s => s.Id == id);

        public List<Sitter> GetSitters() => _context.Sitters.ToList();

        public void RemoveOrRestoreById(int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);

            sitter.IsDeleted = sitter.IsDeleted == true ? false : true;

            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void Update(Sitter updateSitter, int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
 
            sitter.Name = updateSitter.Name;
            sitter.LastName = updateSitter.LastName;
            sitter.Phone = updateSitter.Phone;
            sitter.Age = updateSitter.Age;
            sitter.Experience = updateSitter.Experience;
            sitter.Sex = updateSitter.Sex;
            sitter.Description = updateSitter.Description;
            
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void UpdatePassword(int id, string newPassword)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter.Password = newPassword;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void UpdatePriceCatalog(int id, List<PriceCatalog> newPriceCatalog)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter.PricesCatalog = newPriceCatalog;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }
    }
}
