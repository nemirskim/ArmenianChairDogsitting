using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public class AdminsRepository : IAdminsRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public AdminsRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public int Add(Admin admin)
    {
        _context.Admins.Add(admin);
        _context.SaveChanges();

        return admin.Id;
    }

    public Admin? GetAdminByEmail(string email) => _context.Admins.FirstOrDefault(a => a.Email == email);

    public Admin? GetAdminById(int id) => _context.Admins.FirstOrDefault(a => a.Id == id);

    public void RemoveOrRestoreById(Admin admin)
    {
        _context.Admins.Update(admin);
        _context.SaveChanges();
    }
}
