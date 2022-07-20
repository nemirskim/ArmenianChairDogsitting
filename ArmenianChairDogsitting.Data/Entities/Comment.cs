

namespace ArmenianChairDogsitting.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Text { get; set; }
        public Order Order { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }

    }
}
