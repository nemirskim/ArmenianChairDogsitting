namespace ArmenianChairDogsitting.API.Models.Responses.ClientResponses
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
