namespace ArmenianChairDogsitting.Data.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Order Order { get; set; }
    }
}
