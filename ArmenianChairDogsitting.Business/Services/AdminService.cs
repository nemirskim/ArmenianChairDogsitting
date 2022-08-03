using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Hashing;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Business.Services;

public class AdminService : IAdminService
{
    IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public int AddAdmin(Admin admin)
    {
        admin.Password = PasswordHash.HashPassword(admin.Password);
        return _adminRepository.Add(admin);
    }

    public Admin? GetAdminById(int id) => _adminRepository.GetAdminById(id);

    public void RemoveOrRestoreById(int id, bool isDelete)
    {
        var sitter = _adminRepository.GetAdminById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenSitterDoesNotExist}{id}");

        sitter.IsDeleted = isDelete;

        _adminRepository.RemoveOrRestoreById(sitter);
    }
}
