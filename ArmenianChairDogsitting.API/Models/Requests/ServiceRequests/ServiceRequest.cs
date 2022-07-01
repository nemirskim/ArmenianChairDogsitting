namespace ArmenianChairDogsitting.API.Models
{
    public class ServiceRequest
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal CostPerUnit { get; set; }
        public decimal CostPerDog { get; set; }
        public decimal StartCost { get; set; }
        public int? WalkQuantity { get; set; }
        public int DogQuantity { get; set; }
        public int? VisitQuantity { get; set; }
        public string Promocode { get; set; }
        public bool IsTrial { get; set; }
    }
}
