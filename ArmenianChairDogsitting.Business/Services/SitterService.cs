using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;

namespace ArmenianChairDogsitting.Business.Services;

public class SitterService : ISitterService
{
    ISitterRepository _sitterRepository;

    public SitterService(ISitterRepository sitterRepository)
    {
        _sitterRepository = sitterRepository;
    }

    public int Add(Sitter sitter) => _sitterRepository.Add(sitter);

    public Sitter? GetById(int id) => _sitterRepository.GetById(id);

    public List<Sitter> GetSitters() => _sitterRepository.GetSitters();

    public void RemoveOrRestoreById(int id, bool isDelete)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenSitterDoesNotExist}{id}");

        sitter.IsDeleted = isDelete;

        _sitterRepository.RemoveOrRestoreById(sitter);
    }

    public void Update(Sitter sitterForUpdate, int id)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenSitterDoesNotExist}{id}");

        sitter.Name = sitterForUpdate.Name;
        sitter.LastName = sitterForUpdate.LastName;
        sitter.Phone = sitterForUpdate.Phone;
        sitter.Age = sitterForUpdate.Age;
        sitter.Experience = sitterForUpdate.Experience;
        sitter.Sex = sitterForUpdate.Sex;
        sitter.Description = sitterForUpdate.Description;

        _sitterRepository.Update(sitter);
    }

    public void UpdatePassword(int id, string passwordSitterForUpadate)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenSitterDoesNotExist}{id}");

        sitter.Password = passwordSitterForUpadate;

        _sitterRepository.UpdatePassword(sitter);
    }

    public void UpdatePriceCatalog(int id, Sitter sitterForUpdate)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenSitterDoesNotExist}{id}");

        sitter.PricesCatalog = sitterForUpdate.PricesCatalog;

        _sitterRepository.UpdatePriceCatalog(sitter);
    }
}
