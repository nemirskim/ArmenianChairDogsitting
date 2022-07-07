using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public abstract class AbstractOrderResponse
    {
        [Required]
        public int ClientId { get; set; }
        public int SitterId { get; set; }
        [MinLength(1)]
        [MaxLength(4)]
        public List<DogAllInfoResponse> Animals { get; set; }
        public List<CommentResponse>? Comments { get; set; }
        public Service Type { get; set; }
        public Status Status { get; set; }
    }
}
