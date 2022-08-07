using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class SearchRequest
{
    public decimal PriceMinimum { get; set; }
    public decimal PriceMaximum { get; set; }
    public int MinRating { get; set; }
    public bool IsSitterHasComments { get; set; }
    [EnumRange<Service>]
    public Service ServiceType { get; set; }
    [EnumRange<District>]
    public District? District { get; set; }
}
