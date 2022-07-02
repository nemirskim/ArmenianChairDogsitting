using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class ServiceRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal CostPerUnit { get; set; }
        [Required]
        public decimal CostPerDog { get; set; }
        [Required]
        public decimal StartCost { get; set; }
        [Range(0,60)]
        public int? WalkQuantity { get; set; }
        [Range(1,4)]
        public int DogQuantity { get; set; }
        public int? VisitQuantity { get; set; }
        public string Promocode { get; set; }
        [Required]
        public bool IsTrial { get; set; }
    }
}
