using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class ClientUpdateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string? Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.LastNameIsRequired)]
    public string? LastName { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PhoneIsRequired)]
    [MaxLength(12)]
    public string? Phone { get; set; }
}
