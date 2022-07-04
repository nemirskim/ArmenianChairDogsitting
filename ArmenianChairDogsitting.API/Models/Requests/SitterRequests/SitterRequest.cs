using ArmenianChairDogsitting.API.Enum;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class SitterRequest
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }

    [Required]
    [MinLength(11)]
    [MaxLength(11)]
    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

    [MinLength(8)]
    [MaxLength(30)]
    public string Password { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public int Experience { get; set; }

    [Required]
    public Sex Sex { get; set; }

    public string Description { get; set; }

    public Dictionary<Service, decimal> PriceCatalog { get; set; }
}
