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
    [MaxLength(12)]
    public string? Phone { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLenghtIsLess)]
    public string Password { get; set; }

    //public List<DogMainInfoResponse> Dogs { get; set; }
}