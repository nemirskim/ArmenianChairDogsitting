    using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Service Type { get; set; }
        public Client Client { get; set; }
        public Sitter Sitter { get; set; }
        public Status Status { get; set; }
        public List<Animal> Animals { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime WorkDate { get; set; }
        public Enums.District District { get; set; }
        public int? DayQuantity { get; set; }
        public int? WalkPerDayQuantity { get; set; }
        public int? VisitQuantity { get; set; }
        public bool IsTrial { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
    }
}
