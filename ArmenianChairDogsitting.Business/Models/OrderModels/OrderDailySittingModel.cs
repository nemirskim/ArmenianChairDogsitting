namespace ArmenianChairDogsitting.Business.Models;

public class OrderDailySittingModel : OrderModel
{
    public int DayQuantity { get; set; }
    public int WalkQuantity { get; set; }
}
