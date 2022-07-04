using ArmenianChairDogsitting.API.Enum;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class DogRequest
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [MaxLength(30)]
    public int Age { get; set; }

    public string RecommendationsForCare { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    public string Breed { get; set; }

    [Required]
    public SizeOfAnimal Size { get; set; }
}
