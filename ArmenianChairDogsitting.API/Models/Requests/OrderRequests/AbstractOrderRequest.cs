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
    public ServiceEnum Type { get; set; }
    public Status Status { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public DateTime WorkDate { get; set; }
    public DistrictEnum District { get; set; }
}
