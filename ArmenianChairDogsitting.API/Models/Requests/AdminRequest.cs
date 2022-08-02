using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class AdminRequest
{
    [Required(ErrorMessage = ApiErrorMessage.EmailIsRequired)]
    [EmailAddress(ErrorMessage = ApiErrorMessage.EmailCharacterIsRequired)]
    public string Email { get; set; }
    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLenghtIsLess)]
    public string Password { get; set; }
}
