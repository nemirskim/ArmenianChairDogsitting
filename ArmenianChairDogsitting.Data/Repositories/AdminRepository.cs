using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public AdminRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public Admin? GetAdminByEmail(string email) => _context.Admins.FirstOrDefault(admin => admin.Email == email);
}
