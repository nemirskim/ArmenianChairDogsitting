namespace ArmenianChairDogsitting.Data.Entities;

public class OrderWalk : Order
{
    public int WalkQuantity { get; set; }
    public bool IsTrial { get; set; }
}
