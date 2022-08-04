using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class PriceCatalogRequest
{
    [Required(ErrorMessage = ApiErrorMessage.ServiceIsRequired)]
    public Service Service { get; set; }
    [Required(ErrorMessage = ApiErrorMessage.PriceIsRequired)]
    public decimal Price { get; set; }
}
