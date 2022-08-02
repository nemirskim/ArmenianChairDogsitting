using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface IAdminRepository
{
    public Admin? GetAdminById(int id);
    public Admin? GetAdminByEmail(string email);
    public int Add(Admin admin);
    public void RemoveOrRestoreById(Admin admin);
}
