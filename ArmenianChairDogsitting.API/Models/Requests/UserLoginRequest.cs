using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UserLoginRequest
{
    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLenghtIsLess)]
    public string Password { get; set; }
    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [RegularExpression(Regex.Email)]
    public string Email { get; set; }
}
