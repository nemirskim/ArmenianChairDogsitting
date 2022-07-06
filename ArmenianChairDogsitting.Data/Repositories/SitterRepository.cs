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

        public int AddSitter(Sitter sitter)
        {
            _context.Sitters.Add(sitter);
            _context.SaveChanges();

            return sitter.Id;
        }

        public Sitter? GetSitterById(int id) => _context.Sitters.FirstOrDefault(s => s.Id == id);

        public List<Sitter> GetSitters() => _context.Sitters.ToList();

        public void RemoveOrRestoreSitterById(int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);

            sitter.IsDeleted = sitter.IsDeleted == true ? false : true;

            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void UpdateSitter(Sitter updateSitter, int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter = updateSitter;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void UpdateSitterPassword(int id, string newPassword)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter.Password = newPassword;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void UpdateSitterPriceCatalog(int id, Dictionary<Service, decimal> priceCatalog)
        {
            throw new NotImplementedException();
        }
    }
}
