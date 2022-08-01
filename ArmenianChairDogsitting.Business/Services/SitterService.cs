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

        bool isExist = false;

        if (sitter.PricesCatalog is not null)
        {
            sitter.PricesCatalog.RemoveAll(sitterService =>
            {
                foreach (var service in sitterForUpdate.PricesCatalog)
                {
                    if (service.Service == sitterService.Service)
                        return false;
                }

                return true;
            });
        }

        foreach (var price in sitterForUpdate.PricesCatalog)
        {
            if (sitter.PricesCatalog is not null)
            {
                foreach (var sitterPrice in sitter.PricesCatalog)
                {
                    if (price.Service == sitterPrice.Service)
                    {
                        sitterPrice.Price = price.Price;
                        isExist = true;
                        break;
                    }
                }
            }

            if (isExist)
            {
                isExist = false;
                continue;
            }

            if (sitter.PricesCatalog is null)
                sitter.PricesCatalog = new List<PriceCatalog>();

            sitter.PricesCatalog.Add(new PriceCatalog { Price = price.Price, Service = price.Service, Sitter = new Sitter {Id = id } });
        }

        _sitterRepository.UpdatePriceCatalog(sitter);
    }
}
