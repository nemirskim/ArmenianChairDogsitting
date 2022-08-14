using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class OrderRequest
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int SitterId { get; set; }
    [ListLength(1, 4, ErrorMessage = ApiErrorMessage.DogQuantityError)]
    public List<int> AnimalIds { get; set; }
    public Status Status { get; set; }
    [DateTimeRequired]
    public DateTime WorkDate { get; set; }
    [EnumRange<District>]
    public District District { get; set; }
}
