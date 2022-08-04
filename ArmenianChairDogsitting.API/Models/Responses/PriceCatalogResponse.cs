using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;
public class PriceCatalogResponse
{
    public Service Service { get; set; }
    public decimal Price { get; set; }
}

