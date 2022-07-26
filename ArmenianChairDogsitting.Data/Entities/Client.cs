using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.Data.Entities;

public class Client
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Phone { get; set; }
    [Required]
    public string? Email { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    [Required]
    public string? Password { get; set; }
    public List<Animal> Dogs { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
}
