using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UserUpdatePasswordRequest
{
    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLenghtIsLess)]
    public string Password { get; set; }
    public string OldPassword { get; set; }
}
