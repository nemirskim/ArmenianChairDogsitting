using System.ComponentModel.DataAnnotations;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;

namespace ArmenianChairDogsitting.API.Models;

public abstract class AbstractOrderResponse
{
    public int Id { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int SitterId { get; set; }
    [ListLength(1, 4, ErrorMessage = ApiErrorMessage.DogQuantityError)]
    public List<DogAllInfoResponse> Animals { get; set; }
    public List<CommentResponse> Comments { get; set; }
    public Service Type { get; set; }
    public Status Status { get; set; }
}
