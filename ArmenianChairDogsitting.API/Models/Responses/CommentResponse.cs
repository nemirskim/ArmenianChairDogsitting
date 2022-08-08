using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class CommentResponse
    {
        [Required(ErrorMessage = ApiErrorMessage.IdIsRequired)]
        public int Id { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.IdIsRequired)]
        public int OrderId { get; set; }
        [Range(0, 5, ErrorMessage = ApiErrorMessage.RatingRange)]
        public int Rating { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.TextIsRequired)]
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
    }
}
