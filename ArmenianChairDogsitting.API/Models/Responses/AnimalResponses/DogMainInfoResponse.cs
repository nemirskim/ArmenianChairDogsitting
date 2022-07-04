using ArmenianChairDogsitting.API.Enum;

namespace ArmenianChairDogsitting.API.Models;

public class DogMainInfoResponse : AbstractAnimalResponse
{
    public string Breed { get; set; }
    public SizeOfAnimal Size { get; set; }
}
