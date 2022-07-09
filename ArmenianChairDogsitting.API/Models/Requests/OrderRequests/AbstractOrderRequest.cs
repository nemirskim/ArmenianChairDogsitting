using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Models
{
    public abstract class AbstractOrderRequest
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int SitterId { get; set; }
        [ListLength(1,4)]
        public List<DogAllInfoResponse> Animals { get; set; }
        public List<CommentResponse> Comments { get; set; }
        public Service Type { get; set; }
        public Status Status { get; set; }
    }
}
