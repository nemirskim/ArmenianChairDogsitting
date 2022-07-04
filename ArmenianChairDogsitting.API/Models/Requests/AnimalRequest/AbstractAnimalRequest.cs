namespace ArmenianChairDogsitting.API.Models;

public abstract class AbstractAnimalRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string RecommendationsForCare { get; set; }

}
