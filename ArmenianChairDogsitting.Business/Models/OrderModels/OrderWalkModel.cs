namespace ArmenianChairDogsitting.Business.Models;

public class OrderWalkModel : OrderModel
{
    public int WalkQuantity { get; set; }
    public bool IsTrial { get; set; }
}
