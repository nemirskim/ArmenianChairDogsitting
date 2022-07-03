using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class OrderDailySittingRequest : AbstractOrderRequest
    {
        [Range(1, 60)]
        public int DayQuantity { get; set; }
        [Range(1, 30)]
        public int WalkQuantity { get; set; }
    }
}
