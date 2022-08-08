namespace ArmenianChairDogsitting.API.Models;

public class ClientAllInfoResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Address { get; set; }
}
