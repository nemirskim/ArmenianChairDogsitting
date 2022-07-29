using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class SearchRequest
{
    public decimal PriceMinimum { get; set; }
    public decimal PriceMaximum { get; set; }
    public int MinRating { get; set; }
    public bool IsSitterHasComments { get; set; }
    [EnumRange(typeof(ServiceEnum))]
    public ServiceEnum ServiceType { get; set; }
    [EnumRange(typeof(DistrictEnum))]
    public DistrictEnum District { get; set; }
}
