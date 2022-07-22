using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class SearchRequest
{
    public decimal PriceMinimum { get; set; }
    public decimal PriceMaximum { get; set; }
    public int MinRating { get; set; }
    public bool IsSitterHasComments { get; set; }
    [EnumRange(1, 4)]
    public ServiceEnum ServiceType { get; set; }
    [EnumRange(1, 19)]
    public DistrictEnum District { get; set; }
}
