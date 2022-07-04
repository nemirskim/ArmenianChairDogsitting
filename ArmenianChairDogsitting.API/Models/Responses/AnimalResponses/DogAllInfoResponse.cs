using ArmenianChairDogsitting.API.Enum;

namespace ArmenianChairDogsitting.API.Models;

public class DogAllInfoResponse : AbstractAnimalResponse
{
    public SizeOfAnimal Size { get; set; }
}
