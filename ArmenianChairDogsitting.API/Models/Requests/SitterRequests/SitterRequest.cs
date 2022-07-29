using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class SitterRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.LastNameIsRequired)]
    public string LastName { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PhoneIsRequired)]
    [MinLength(11, ErrorMessage = ApiErrorMessage.PhoneIsRange)]
    public string Phone { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.EmailIsRequired)]
    [EmailAddress(ErrorMessage = ApiErrorMessage.EmailCharacterIsRequired)]
    public string Email { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequired)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLenghtIsLess)]
    public string Password { get; set; }

    [Required]
    [Range(1, 130, ErrorMessage = ApiErrorMessage.AgeIsRange)]
    public int Age { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.ExperienceIsRequired)]
    public int Experience { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.SexIsRequired)]
    public Sex Sex { get; set; }

    public string Description { get; set; }
}
