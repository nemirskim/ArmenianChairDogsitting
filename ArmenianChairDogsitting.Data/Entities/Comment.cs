

namespace ArmenianChairDogsitting.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public Client ClientId { get; set; }
        public string Title { get; set; }
        public Order Order { get; set; }
        public DateTime TimeCreated { get; set; }
        public bool IsDeleted { get; set; }

    }
}
