using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;
//цена услуг, общая оценка, количество отзывов, районы, породы собак.
public class Search
{
    public decimal PriceMinimum { get; set; }
    public decimal PriceMaximum { get; set; }
    public int Rating { get; set; }
    public  int CommentsQuantity { get; set; }
    public District District { get; set; }
    
    //породы
}
