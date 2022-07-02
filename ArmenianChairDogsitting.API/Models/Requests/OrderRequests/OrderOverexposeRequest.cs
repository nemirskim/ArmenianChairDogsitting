using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class OrderOverexposeRequest : AbstractOrder
    {
        [Range(1,60)]
        public int DayQuantity { get; set; }
        [Required]
        public int WalkPerDayQuantity { get; set; }
    }
}
