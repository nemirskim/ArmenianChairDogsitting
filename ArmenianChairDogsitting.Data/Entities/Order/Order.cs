using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities
{
    public abstract class Order
    {
        public int Id { get; set; }
        public ServiceEnum Type { get; set; }
        public Client Client { get; set; }
        public Sitter Sitter { get; set; }
        public List<Animal> Animals { get; set; }
        public List<Comment>? Comments { get; set; }
        public Status Status { get; set; }
    }
}
