using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface IPromocodeRepository
{
    Promocode? GetPromocode(string promocode);
}
