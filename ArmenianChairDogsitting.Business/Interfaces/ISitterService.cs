using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ISitterService
{
    public Sitter? GetById(int id);
    public List<Sitter> GetSitters();
    public int Add(Sitter sitter);
    public void Update(Sitter sitter, int id);
    public void RemoveOrRestoreById(int id);
    public void UpdatePriceCatalog(int id, List<PriceCatalog> priceCatalog);
    public void UpdatePassword(int id, string password);
}
