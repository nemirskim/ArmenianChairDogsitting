namespace ArmenianChairDogsitting.API.Models


{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
