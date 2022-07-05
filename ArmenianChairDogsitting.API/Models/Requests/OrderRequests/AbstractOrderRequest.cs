using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public abstract class AbstractOrderRequest
    {
        [Required]
        public int ClientId { get; set; }
        [MinLength(1)]
        [MaxLength(4)]
        public List<DogRequest> Animals { get; set; }
        public Status Status { get; set; }
    }
}
