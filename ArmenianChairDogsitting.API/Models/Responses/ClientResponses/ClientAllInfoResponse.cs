namespace ArmenianChairDogsitting.API.Models;   

public class ClientAllInfoResponse
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public List<DogAllInfoResponse> Animals { get; set; }
}
