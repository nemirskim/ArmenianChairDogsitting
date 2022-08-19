using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class DogMainInfoResponse : DogAllInfoResponse
{
    public int Id { get; set; }
    public int Age { get; set; }
    public SizeOfAnimal Size { get; set; }
    public string RecommendationsForCare { get; set; }
    public bool IsDeleted { get; set; }
}
