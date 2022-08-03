using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ISittersService
{
    public Sitter? GetById(int id);
    public List<Sitter> GetSitters();
    public int Add(Sitter sitter);
    public void Update(Sitter sitter, int id);
    public void RemoveOrRestoreById(int id, bool isDelete);
    public void UpdatePriceCatalog(int id, Sitter sitterForUpdate);
    public void UpdatePassword(int id, string password);
}
