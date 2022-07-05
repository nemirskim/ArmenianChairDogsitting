using ArmenianChairDogsitting.API.Enum;

namespace ArmenianChairDogsitting.API.Models;

public class DogMainInfoResponse : DogAllInfoResponse
{
    public int Age { get; set; }
    public string RecommendationsForCare { get; set; }
    public SizeOfAnimal Size { get; set; }
}
