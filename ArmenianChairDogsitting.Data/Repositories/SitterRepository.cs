using ArmenianChairDogsitting.Data.Entities;
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

        public Sitter? GetSitterById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sitter> GetSitters() => _context.Sitters.ToList();

        public void RemoveSitterById(int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter.IsDeleted = true;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void RestoreSitterById(int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter.IsDeleted = false;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }

        public void UpdateSitter(Sitter UpdateSitter, int id)
        {
            var sitter = _context.Sitters.FirstOrDefault(s => s.Id == id);
            sitter = UpdateSitter;
            _context.Sitters.Update(sitter);
            _context.SaveChanges();
        }
    }
}
