using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Business.Models;

public class SittersSearchModelResult
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Experience { get; set; }
    public List<District> Districts { get; set; }
    public List<Comment> Comments { get; set; }
}
