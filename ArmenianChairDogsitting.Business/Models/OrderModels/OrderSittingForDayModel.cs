namespace ArmenianChairDogsitting.Business.Models;

public class OrderSittingForDayModel : OrderModel
{
    public int HourQuantity { get; set; }
    public int WalkQuantity { get; set; }
    public int VisitQuantity { get; set; }
}
