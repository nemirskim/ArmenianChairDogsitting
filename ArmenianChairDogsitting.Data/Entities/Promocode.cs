namespace ArmenianChairDogsitting.Data.Entities
{
    public class Promocode
    {
        public int Id { get; set; }
        public string Promo { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }    
    }
}
