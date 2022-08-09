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
    [EnumRange<Service>]
    public Service Type { get; set; }
    [DateTimeRequired]
    public DateTime WorkDate { get; set; }
    [EnumRange<District>]
    public District District { get; set; }
    [Range(1, 60)]
    public int DayQuantity { get; set; }
    [Range(1, 30)]
    public int WalkQuantity { get; set; }
    [Required]
    public int WalkPerDayQuantity { get; set; }
    public bool IsTrial { get; set; }
    [Range(1, 24)]
    public int HourQuantity { get; set; }
    [Range(1, 24)]
    public int VisitQuantity { get; set; }
}
