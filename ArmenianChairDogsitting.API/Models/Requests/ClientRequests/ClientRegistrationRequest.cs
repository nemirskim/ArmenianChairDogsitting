using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class ClientRegistrationRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string? Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.LastNameIsRequired)]
    public string? LastName { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PhoneIsRequired)]
    [RegularExpression(Regex.PhoneNumber)]
    public string? Phone { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [RegularExpression(Regex.Email)]
    public string? Email { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLenghtIsLess)]
    public string Password { get; set; }
    [StringLength(50, MinimumLength = 10)]
    public string Address { get; set; }
    public string Promocode { get; set; }
}