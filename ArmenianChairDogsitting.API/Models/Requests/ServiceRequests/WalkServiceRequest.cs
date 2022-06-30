namespace ArmenianChairDogsitting.API.Models
{
    public class WalkServiceRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CostPerWalk { get; set; }
        public decimal CostPerDog { get; set; }
        public decimal StartCost { get; set; }
        public int WalkQuantity { get; set; }
        public int DogQuantity { get; set; }
        public List<string> Promocodes { get; set; }
        public bool IsTrial { get; set; }
    }
}
