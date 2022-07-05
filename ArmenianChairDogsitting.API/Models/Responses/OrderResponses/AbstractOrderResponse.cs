using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public abstract class AbstractOrderResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [MinLength(1)]
        [MaxLength(4)]
        public List<DogAllInfoResponse> Animals { get; set; }
        public Status Status { get; set; }
    }
}
