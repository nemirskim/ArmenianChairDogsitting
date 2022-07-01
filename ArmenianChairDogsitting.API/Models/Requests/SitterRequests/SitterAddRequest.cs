namespace ArmenianChairDogsitting.API.Models;

public class SitterAddRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public int Experience { get; set; }
    public string Sex { get; set; }
}
