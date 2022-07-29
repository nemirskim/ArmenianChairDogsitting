using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Repositories.Interfaces;

public interface ISitterRepository
{
    public Sitter? GetById(int id);
    public List<Sitter> GetSitters();
    public int Add(Sitter sitter);
    public void Update(Sitter newSitter);
    public void RemoveOrRestoreById(Sitter sitter);
    public void UpdatePriceCatalog(Sitter sitterWithNewPriceCatalog);
    public void UpdatePassword(Sitter newPassword);
}

