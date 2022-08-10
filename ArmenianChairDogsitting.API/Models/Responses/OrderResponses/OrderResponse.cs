using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class OrderResponse
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SitterId { get; set; }
    public List<DogAllInfoResponse> Animals { get; set; }
    public List<CommentResponse> Comments { get; set; }
    public District District { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime DateUpdated { get;  set; }
    public decimal Price { get; set; }
    public Service Type { get; set; }
    public Status Status { get; set; }
    public bool IsDeleted { get; set; }
    public int DayQuantity { get; set; }
    public int WalkQuantity { get; set; }
    public int WalkPerDayQuantity { get; set; }
    public int HourQuantity { get; set; }
    public int VisitQuantity { get; set; }
    public bool IsTrial { get; set; }
}
