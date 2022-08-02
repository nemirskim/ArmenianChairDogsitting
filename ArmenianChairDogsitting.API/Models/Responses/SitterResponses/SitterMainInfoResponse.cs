using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class SitterMainInfoResponse
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public int Experience { get; set; }
    public Sex Sex { get; set; }
    public string Description { get; set; }
    public List<PriceCatalogResponse> PriceCatalog { get; set; }
}
