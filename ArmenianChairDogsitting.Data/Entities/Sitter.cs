using ArmenianChairDogsitting.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Entities;

public class Sitter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public int Experience { get; set; }
    public Sex Sex { get; set; }
    public string Description { get; set; }
    public List<District> Districts { get; set; }
    public List<PriceCatalog> PricesCatalog { get; set; }
    public List<Order> Orders { get; set; } 
    public bool IsDeleted {  get; set; }
}
