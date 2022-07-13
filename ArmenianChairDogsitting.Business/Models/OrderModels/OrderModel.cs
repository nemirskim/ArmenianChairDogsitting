using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Business.Models;

public abstract class OrderModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SitterId { get; set; }
    public List<AnimalModel> Animals { get; set; }
    public List<CommentModel> Comments { get; set; }
    public ServiceEnum Type { get; set; }
    public Status Status { get; set; }
}
