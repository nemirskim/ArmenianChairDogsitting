using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class PriceCatalogRequest
{
    [Required(ErrorMessage = ApiErrorMessage.ServiceIsRequired)]
    [EnumRange<Service>]
    public Service Service { get; set; }
    [Required(ErrorMessage = ApiErrorMessage.PriceIsRequired)]
    [Range(typeof(decimal), "1", "100000")]
    public decimal Price { get; set; }
}
