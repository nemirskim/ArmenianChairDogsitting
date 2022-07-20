using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;
//цена услуг, общая оценка, наличие отзывов, районы, породы собак.
public class SearchParams
{
    public decimal PriceMinimum { get; set; }
    public decimal PriceMaximum { get; set; }
    public int MinRating { get; set; }
    public bool IsSitterHasComments { get; set; }
    public ServiceEnum ServiceType { get; set; }
    public DistrictEnum District { get; set; }
    
    //породы
}
