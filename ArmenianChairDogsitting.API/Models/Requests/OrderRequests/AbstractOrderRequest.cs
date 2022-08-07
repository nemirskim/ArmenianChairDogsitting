using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;

namespace ArmenianChairDogsitting.API.Models;

public abstract class AbstractOrderRequest
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int SitterId { get; set; }
    [ListLength(1, 4, ErrorMessage = ApiErrorMessage.DogQuantityError)]
    public List<int> AnimalIds { get; set; }
    public Status Status { get; set; }
    [EnumRange<Service>]
    public Service Type { get; set; }
    [DateTimeRequired]
    public DateTime WorkDate { get; set; }
    [EnumRange<District>]
    public District District { get; set; }
}
