using ArmenianChairDogsitting.API.Enum;

namespace ArmenianChairDogsitting.API.Models;

public class DogRequest : AbstractAnimalRequest
{
    public string Breed { get; set; }
    public SizeOfAnimal Size { get; set; }
}
