namespace ArmenianChairDogsitting.Data.Entities;

public class OrderSittingForDay : Order
{
    public int HourQuantity { get; set; }
    public int WalkQuantity { get; set; }
    public int VisitQuantity { get; set; }
}
