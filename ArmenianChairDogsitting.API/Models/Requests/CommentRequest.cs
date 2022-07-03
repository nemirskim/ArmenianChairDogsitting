using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class CommentRequest
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Range(0,5)]
        public int Rating { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
