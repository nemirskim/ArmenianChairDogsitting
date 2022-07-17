using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class DogUpdateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.AgeIsRequired)]
    [Range(1, 25, ErrorMessage = ApiErrorMessage.AgeIsRange)]
    public int Age { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.BreedIsRequired)]
    public string Breed { get; set; }

    public string RecommendationsForCare { get; set; }
}
