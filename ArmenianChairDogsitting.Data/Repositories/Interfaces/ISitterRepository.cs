using ArmenianChairDogsitting.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Repositories.Interfaces
{
    public interface ISitterRepository
    {
        public Sitter? GetSitterById(int id);
        public List<Sitter> GetSitters();
        public int AddSitter(Sitter sitter);
        public void UpdateSitter(Sitter sitter, int id);
        public void RemoveSitterById(int id);
        public void RestoreSitterById(int id);
    }
}
