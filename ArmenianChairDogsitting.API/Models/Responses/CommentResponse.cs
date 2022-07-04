using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class CommentResponse
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
