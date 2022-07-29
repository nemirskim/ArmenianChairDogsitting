using ArmenianChairDogsitting.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class SitterUpdatePriceCatalogRequest
{
    [Required]
    public List<PriceCatalog> PriceCatalog { get; set; }
}
