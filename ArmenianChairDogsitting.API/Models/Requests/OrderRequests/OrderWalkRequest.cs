using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class OrderWalkRequest
    {
        [Range(1, 60)]
        public int WalkQuantity { get; set; }
        public bool IsTrial { get; set; }
    }
}
