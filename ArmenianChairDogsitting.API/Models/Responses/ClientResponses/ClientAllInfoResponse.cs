namespace ArmenianChairDogsitting.API.Models;   

public class ClientAllInfoResponse
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public List<AnimalAllInfoResponse> Animals { get; set; }
}
