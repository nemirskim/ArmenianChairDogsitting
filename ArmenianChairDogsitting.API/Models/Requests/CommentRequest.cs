using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class CommentRequest
    {
        [Range(0,5)]
        public int Rating { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
