namespace ArmenianChairDogsitting.Data.Entities;

public class OrderDailySitting : Order
{
    public int DayQuantity { get; set; }
    public int WalkQuantity { get; set; }
}
