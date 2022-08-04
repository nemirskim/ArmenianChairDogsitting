using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;
public class PriceCatalogResponse
{
    public ServiceEnum Service { get; set; }
    public decimal Price { get; set; }
}

