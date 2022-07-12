using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Seed;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.Data.Entities;

public class Service : IEnumModel<Service, ServiceEnum>
{
    public ServiceEnum Id { get; set; }
    public List<Sitter> Sitters { get; set; }
}
