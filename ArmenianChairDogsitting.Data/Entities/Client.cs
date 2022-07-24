namespace ArmenianChairDogsitting.Data.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Pasword { get; set; }
    public string Email {  get; set; }
    public List<Animal> Dogs { get; set; }
    public bool IsDeleted { get; set; }
}
