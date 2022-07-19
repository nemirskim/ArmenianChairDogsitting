using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Business;

public class ClientModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public List<DogModel> Dogs { get; set; }
    public List<OrderModel> Orders { get; set; }
    public List<CommentModel> Comments { get; set; }
}
