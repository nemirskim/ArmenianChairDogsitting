using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Models;

public class ClientUpdateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string? Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.LastNameIsRequired)]
    public string? LastName { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PhoneIsRequired)]
    [MaxLength(12)]
    [RegularExpression(Regex.PhoneNumber,
         ErrorMessage = ApiErrorMessage.InvalidPhoneNumber)]
    public string? Phone { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.AddressIsRequired)]
    public string? Address { get; set; }
}
