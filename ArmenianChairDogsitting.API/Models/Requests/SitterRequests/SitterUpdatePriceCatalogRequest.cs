using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class SitterUpdatePriceCatalogRequest
{
    [Required(ErrorMessage = ApiErrorMessage.PriceCatalogIsRequired)]
    public List<PriceCatalogRequest> PriceCatalog { get; set; }
}
