using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface ISitterRepository
{
    public Sitter? GetById(int id);
    public List<Sitter> GetSitters();
    public int Add(Sitter sitter);
    public void Update(Sitter sitter, int id);
    public void RemoveOrRestoreById(int id);
    public void UpdatePriceCatalog(int id, List<PriceCatalog> priceCatalog);
    public void UpdatePassword(int id, string password);
    public Sitter? GetSitterByEmail(string email);
}
