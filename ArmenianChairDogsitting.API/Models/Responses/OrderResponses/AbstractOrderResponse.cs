using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public abstract class AbstractOrderResponse
    {
        [Required]
        public int ClientId { get; set; }
        [MinLength(1)]
        [MaxLength(4)]
        public List<AnimalAllInfoResponse> Animals { get; set; }
        public Status Status { get; set; }
    }
}
