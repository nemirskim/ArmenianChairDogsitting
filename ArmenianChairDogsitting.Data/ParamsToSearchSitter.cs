using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;

public class ParamsToSearchSitter
{
    public decimal? PriceMinimum { get; set; }
    public decimal? PriceMaximum { get; set; }
    public int? MinRating { get; set; }
    public bool IsSitterHasComments { get; set; }
    public Service ServiceType { get; set; }
    public Enums.District District { get; set; }
    
    //породы
}
