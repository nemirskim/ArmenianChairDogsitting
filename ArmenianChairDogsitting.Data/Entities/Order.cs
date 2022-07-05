using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int ClientId { get; set; }
        public List<Animal> Animals { get; set; }
        public List<Comment> Comments { get; set; }
        public Status Status { get; set; }
        public int? DayQuantity { get; set; }
        public int? WalkQuantity { get; set; }
        public int? WalkPerDayQuantity { get; set; }
        public int? HourQuantity { get; set; }
        public int? VisitQuantity { get; set; }
        public bool? IsTrial { get; set; }
    }
}
