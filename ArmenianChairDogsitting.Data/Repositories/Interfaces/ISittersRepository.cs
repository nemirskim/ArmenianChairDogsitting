using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories.Interfaces;

public interface ISittersRepository
{
    public Sitter? GetById(int id);
    public List<Sitter> GetSitters();
    public int Add(Sitter sitter);
    public void Update(Sitter newSitter);
    public void RemoveOrRestoreById(Sitter sitter);
    public void UpdatePriceCatalog(Sitter sitterWithNewPriceCatalog);
    public void UpdatePassword(Sitter newPassword);
    public Sitter? GetSitterByEmail(string email);
}