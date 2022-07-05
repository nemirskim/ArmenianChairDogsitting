using ArmenianChairDogsitting.API.Enum;
using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class DogRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.AgeIsRequired)]
    [Range(1, 25, ErrorMessage = ApiErrorMessage.AgeIsRange)]
    public int Age { get; set; }

    public string RecommendationsForCare { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.BreedIsRequired)]
    public string Breed { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.SizeIsRequired)]
    public SizeOfAnimal Size { get; set; }
}
