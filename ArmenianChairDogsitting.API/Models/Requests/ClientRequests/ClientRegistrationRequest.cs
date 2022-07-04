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
    [MaxLength(16)]
    public string Password { get; set; }

    public List<DogMainInfoResponse> Dogs { get; set; }
}
