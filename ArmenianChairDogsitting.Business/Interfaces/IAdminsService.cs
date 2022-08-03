
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface IAdminsService
{
    public int AddAdmin(Admin admin);
    public void RemoveOrRestoreById(int id, bool isDelete);
    public Admin? GetAdminById(int id);
}
