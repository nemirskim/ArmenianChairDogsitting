using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class AbstractOrder
    {
        [Required]
        public int ClientId { get; set; }
        [MinLength(1)]
        [MaxLength(4)]
        public List<Animal> Animals { get; set; }
    }
}
