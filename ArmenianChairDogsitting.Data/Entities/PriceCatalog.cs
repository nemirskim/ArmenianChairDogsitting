using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;

public class PriceCatalog
{
    public int Id { get; set; }
    public ServiceEnum Service { get; set; }
    public decimal Price { get; set; }
    public Sitter Sitter { get; set; }
}
