using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Models
{
    public abstract class AbstractOrderResponse
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int SitterId { get; set; }
        [ListLength(1, 4, ErrorMessage = "Собак должно быть в колличестве от 1 до 4")]
        public List<DogAllInfoResponse> Animals { get; set; }
        public List<CommentResponse> Comments { get; set; }
        public Service Type { get; set; }
        public Status Status { get; set; }
    }
}
