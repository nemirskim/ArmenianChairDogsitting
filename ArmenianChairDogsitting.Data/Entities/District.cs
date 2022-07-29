using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Seed;

namespace ArmenianChairDogsitting.Data.Entities;

public class District : IEnumModel<District, DistrictEnum>
{
    public DistrictEnum Id { get; set; }
    public List<Sitter> Sitters { get; set; }
}