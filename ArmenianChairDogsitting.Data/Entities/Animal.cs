using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public SizeOfAnimal Size { get; set; }
    public string RecommendationsForCare { get; set; }
    public int ClientId { get; set; }
    public bool IsDeleted { get; set; }
    //public List<Order> Orders { get; set; }
}
