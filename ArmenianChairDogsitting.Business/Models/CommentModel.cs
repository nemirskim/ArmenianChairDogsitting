using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Models;

public class CommentModel
{
    public int Id { get; set; }
    public ClientModel Client { get; set; }
    public string Text { get; set; }
    public OrderModel Order { get; set; }
    public DateTime TimeCreated { get; set; }
    public DateTime TimeUpdated { get; set; }
    public int Rating { get; set; }
    public bool IsDeleted { get; set; }
}
