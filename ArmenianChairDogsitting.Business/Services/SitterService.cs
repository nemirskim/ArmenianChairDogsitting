using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Business.ExceptionsStorage;
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

    public Sitter? GetById(int id)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionStorage.ChoosenSitterDoesNotExist}{id}");

        return sitter;
    }

    public List<Sitter> GetSitters() => _sitterRepository.GetSitters();

    public void RemoveOrRestoreById(int id)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionStorage.ChoosenSitterDoesNotExist}{id}");

        _sitterRepository.RemoveOrRestoreById(id);
    }

    public void Update(Sitter sitterForUpdate, int id)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionStorage.ChoosenSitterDoesNotExist}{id}");

        _sitterRepository.Update(sitterForUpdate, id);
    }

    public void UpdatePassword(int id, string password)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionStorage.ChoosenSitterDoesNotExist}{id}");

        _sitterRepository.UpdatePassword(id, password);
    }

    public void UpdatePriceCatalog(int id, List<PriceCatalog> priceCatalog)
    {
        var sitter = _sitterRepository.GetById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionStorage.ChoosenSitterDoesNotExist}{id}");

        _sitterRepository.UpdatePriceCatalog(id, priceCatalog);
    }
}
