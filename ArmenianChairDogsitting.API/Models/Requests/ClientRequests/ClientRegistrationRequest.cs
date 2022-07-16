using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class ClientRegistrationRequest
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(12)]
    public string? Phone { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
