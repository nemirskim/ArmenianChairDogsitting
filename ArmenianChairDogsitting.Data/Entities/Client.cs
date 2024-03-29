﻿using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string? Password { get; set; }
    public List<Animal> Dogs { get; set; }
    public List<Order> Orders { get; set; }
    public Role Role { get; set; }
    public string? Promocode { get; set; }
    public bool IsDeleted { get; set; }
}
