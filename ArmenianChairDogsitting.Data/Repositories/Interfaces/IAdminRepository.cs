using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface IAdminRepository
{
    public Admin? GetAdminByEmail(string email);
}
