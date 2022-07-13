using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class CommentRequest
    {
        [Range(0,5, ErrorMessage = ApiErrorMessage.RatingRange)]
        public int Rating { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.TextIsRequired)]
        public string Text { get; set; }
    }
}
